using System;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.CrewmateRoles.ChameleonMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__14), nameof(IntroCutscene._CoBegin_d__14.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__14 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Chameleon))
            {
                var chameleon = (Chameleon) role;
                chameleon.LastSwooped = DateTime.UtcNow;
                chameleon.LastSwooped = chameleon.LastSwooped.AddSeconds(-10f);
            }
        }
    }
}