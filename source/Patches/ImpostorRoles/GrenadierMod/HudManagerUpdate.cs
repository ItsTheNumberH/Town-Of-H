using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;
using System.Linq;

namespace TownOfUs.ImpostorRoles.GrenadierMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite FlashSprite => TownOfUs.FlashSprite;

        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Grenadier)) return;
            var role = Role.GetRole<Grenadier>(PlayerControl.LocalPlayer);
            if (role.FlashButton == null)
            {
                try {
                    role.FlashButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                    role.FlashButton.renderer.enabled = true;
                } catch {
                }
            }

            try {
                role.FlashButton.renderer.sprite = FlashSprite;
                role.FlashButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
                var position = __instance.KillButton.transform.localPosition;
                role.FlashButton.transform.localPosition = new Vector3(position.x,
                    __instance.ReportButton.transform.localPosition.y, position.z);

                if (role.Flashed)
                {
                    role.FlashButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.GrenadeDuration);
                    return;
                }
            } catch {
            }

            //To stop the scenario where the flash and sabotage are called at the same time.
            try {
                var system = ShipStatus.Instance.Systems[SystemTypes.Sabotage].Cast<SabotageSystemType>();
                var specials = system.specials.ToArray();
                var dummyActive = system.dummy.IsActive;
                var sabActive = specials.Any(s => s.IsActive);
                if (sabActive) {
                    role.FlashButton.renderer.color = Palette.DisabledClear;
                } else {
                    role.FlashButton.renderer.color = Palette.EnabledColor;
                }
                role.FlashButton.SetCoolDown(role.FlashTimer(), CustomGameOptions.GrenadeCd);
                role.FlashButton.renderer.material.SetFloat("_Desat", 0f);
            } catch {
            }
        }
    }
}