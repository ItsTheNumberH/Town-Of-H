using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUs.ImpostorRoles.FreezerMod {
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate {
        private static readonly int Desat = Shader.PropertyToID("_Desat");

        public static void Postfix(HudManager __instance) {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Freezer)) return;
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            var role = Role.GetRole<Freezer>(PlayerControl.LocalPlayer);
            if (role._freezeButton == null) {
                role._freezeButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role._freezeButton.renderer.enabled = true;
                role._freezeButton.renderer.sprite = Freezer._freezeSprite;
            }

            role._freezeButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            role._freezeButton.transform.localPosition = __instance.KillButton.transform.localPosition;

            __instance.KillButton.renderer.color = new Color(0, 0, 0, 0);
            __instance.KillButton.TimerText.color = new Color(0, 0, 0, 0);
            __instance.KillButton.gameObject.SetActive(false);

            var notImpostor = PlayerControl.AllPlayerControls.ToArray().Where(
                x => !x.Is(Faction.Impostors)
            ).ToList();
            Utils.SetTarget(ref role.ClosestPlayer, role._freezeButton, float.NaN, notImpostor);
            
            if (role.ClosestPlayer) {
                role._freezeButton.renderer.color = Palette.EnabledColor;
                role._freezeButton.renderer.material.SetFloat(Desat, 0f);
            }
            else {
                role._freezeButton.renderer.color = Palette.DisabledClear;
                role._freezeButton.renderer.material.SetFloat(Desat, 1.0f);
            }
        }
    }
}