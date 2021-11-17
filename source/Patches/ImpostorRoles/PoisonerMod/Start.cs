using System;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__14), nameof(IntroCutscene._CoBegin_d__14.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__14 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Poisoner))
            {
                var poisoner = (Poisoner) role;
                poisoner.LastPoisoned = DateTime.UtcNow;
                poisoner.LastPoisoned = poisoner.LastPoisoned.AddSeconds(CustomGameOptions.InitialPoisonerCooldown - CustomGameOptions.PoisonerCd);
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner)) {
                    DestroyableSingleton<HudManager>.Instance.KillButton.enabled = false;
                    DestroyableSingleton<HudManager>.Instance.KillButton.renderer.enabled = false;
                }
            }
        }
    }
}