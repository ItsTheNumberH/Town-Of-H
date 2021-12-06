using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.ImpostorRoles.FreezerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class PlayerControlUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Freezer)) return;

            var role = Role.GetRole<Freezer>(PlayerControl.LocalPlayer);
            if (role.FreezeButton == null)
            {
                role.FreezeButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role.FreezeButton.renderer.enabled = true;
                role.FreezeButton.renderer.sprite = TownOfUs.FreezeSprite;
            }

            role.FreezeButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            role.FreezeButton.transform.localPosition = __instance.KillButton.transform.localPosition;

            role.FreezeButton.SetCoolDown(role.Player.killTimer, CustomGameOptions.FreezerCooldown);
        }
    }
}