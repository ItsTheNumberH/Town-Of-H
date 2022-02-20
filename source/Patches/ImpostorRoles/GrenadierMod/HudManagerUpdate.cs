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
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Grenadier)) return;
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            var role = Role.GetRole<Grenadier>(PlayerControl.LocalPlayer);
            if (role.FlashButton == null)
            {
                role.FlashButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.FlashButton.graphic.enabled = true;
                role.FlashButton.GetComponent<AspectPosition>().DistanceFromEdge = TownOfUs.ButtonPosition;
                role.FlashButton.gameObject.SetActive(false);
            }

            role.FlashButton.GetComponent<AspectPosition>().Update();
            role.FlashButton.graphic.sprite = FlashSprite;
            role.FlashButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);

            if (role.Flashed)
            {
                role.FlashButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.GrenadeDuration);
                return;
            }

            role.FlashButton.SetCoolDown(role.FlashTimer(), CustomGameOptions.GrenadeCd);

            var system = ShipStatus.Instance.Systems[SystemTypes.Sabotage].Cast<SabotageSystemType>();
            var specials = system.specials.ToArray();
            var dummyActive = system.dummy.IsActive;
            var sabActive = specials.Any(s => s.IsActive);

            if (sabActive & !dummyActive)
            {
                role.FlashButton.graphic.color = Palette.DisabledClear;
                role.FlashButton.graphic.material.SetFloat("_Desat", 1f);
                return;
            }

            role.FlashButton.graphic.color = Palette.EnabledColor;
            role.FlashButton.graphic.material.SetFloat("_Desat", 0f);
        }
    }
}