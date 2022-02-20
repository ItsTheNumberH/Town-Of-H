using System;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__18), nameof(IntroCutscene._CoBegin_d__18.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__18 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Poisoner))
            {
                var poisoner = (Poisoner) role;
                poisoner.LastPoisoned = DateTime.UtcNow;
                poisoner.LastPoisoned = poisoner.LastPoisoned.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.PoisonCd);
            }
        }
    }
}