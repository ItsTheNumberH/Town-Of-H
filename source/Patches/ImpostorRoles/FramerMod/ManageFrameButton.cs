using System;
using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUs.Patches.ImpostorRoles.FramerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class ManageFrameButton
    {
        public static void Postfix(HudManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Framer)) return;
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;

            Framer role = Role.GetRole<Framer>(PlayerControl.LocalPlayer);
            if (role.FrameButton == null)
            {
                role.FrameButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role.FrameButton.renderer.enabled = true;
            }

            role.FrameButton.renderer.sprite = TownOfUs.FrameSprite;

            role.FrameButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            var position = __instance.KillButton.transform.localPosition;
            role.FrameButton.transform.localPosition = new Vector3(position.x,
                __instance.ReportButton.transform.localPosition.y, position.z);

            if (role.Framed != null)
            {
                // TODO: Not entirely accurate?
                role.FrameButton.SetCoolDown(role.TimeBeforeFramed + role.FrameTimeRemaining, CustomGameOptions.FrameDuration);
                return;
            }

            Utils.SetTarget(ref role.Target, role.FrameButton);

            role.FrameButton.SetCoolDown(role.FrameTimer(), CustomGameOptions.FrameCooldown);
            role.FrameButton.renderer.color = Palette.EnabledColor;
        }
    }
}
