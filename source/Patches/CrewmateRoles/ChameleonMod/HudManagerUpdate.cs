using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.ChameleonMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite SwoopSprite => TownOfUs.HideSprite;

        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Chameleon)) return;
            var role = Role.GetRole<Chameleon>(PlayerControl.LocalPlayer);
            if (role.SwoopButton == null)
            {
                role.SwoopButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role.SwoopButton.renderer.enabled = true;
            }

            role.SwoopButton.renderer.sprite = SwoopSprite;
            role.SwoopButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            var position = __instance.KillButton.transform.localPosition;
            role.SwoopButton.transform.localPosition = new Vector3(position.x - 1.3f,
                __instance.ReportButton.transform.localPosition.y - 1.3f, position.z);

            if (role.IsSwooped)
            {
                role.SwoopButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.ChameleonDuration);
                return;
            }

            role.SwoopButton.SetCoolDown(role.SwoopTimer(), CustomGameOptions.ChameleonCd);


            role.SwoopButton.renderer.color = Palette.EnabledColor;
            role.SwoopButton.renderer.material.SetFloat("_Desat", 0f);
        }
    }
}