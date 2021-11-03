using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite PoisonSprite => TownOfUs.PoisonSprite;
        public static Sprite PoisonedSprite => TownOfUs.PoisonedSprite;
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner)) return;
            var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
            if (role.PoisonButton == null) {
                role.PoisonButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role.PoisonButton.renderer.enabled = true;
                role.PoisonButton.renderer.sprite = PoisonSprite;
            }

            role.PoisonButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            var position = __instance.KillButton.transform.localPosition;
            role.PoisonButton.transform.localPosition = new Vector3(position.x,
                __instance.ReportButton.transform.localPosition.y, position.z);
            Utils.SetTarget(ref role.ClosestPlayer, role.PoisonButton);

            if (role.ClosestPlayer != null) {
                role.ClosestPlayer.myRend.material.SetColor("_OutlineColor", Palette.Purple);
            }

            role.Player.SetKillTimer(0);
            try {
                if (role.Poisoned) {
                    role.PoisonButton.renderer.sprite = PoisonedSprite;
                    role.Poison();
                    role.PoisonButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.PoisonerDuration);
                } else {
                    role.PoisonButton.renderer.sprite = PoisonSprite;
                    if (role.PoisonedPlayer && !role.PoisonedPlayer.Data.IsDead && !role.PoisonedPlayer == PlayerControl.LocalPlayer) {
                        role.PoisonKill();
                    }
                    if (role.ClosestPlayer != null) {
                        role.PoisonButton.renderer.color = Palette.EnabledColor;
                        role.PoisonButton.renderer.material.SetFloat("_Desat", 0f);
                    } else {
                        role.PoisonButton.renderer.color = Palette.DisabledClear;
                        role.PoisonButton.renderer.material.SetFloat("_Desat", 1f);
                    }
                    role.PoisonButton.SetCoolDown(role.PoisonTimer(), CustomGameOptions.PoisonerCd);
                    role.PoisonedPlayer = PlayerControl.LocalPlayer; //Only do this to stop repeatedly trying to re-kill poisoned player. null didn't work for some reason
                }
            } catch {

            }
        }
    }
}
