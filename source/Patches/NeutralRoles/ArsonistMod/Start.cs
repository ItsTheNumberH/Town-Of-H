using System;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.NeutralRoles.ArsonistMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__14), nameof(IntroCutscene._CoBegin_d__14.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__14 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
            {
                var arsonist = (Arsonist) role;
                arsonist.LastDoused = DateTime.UtcNow;
                arsonist.LastDoused = arsonist.LastDoused.AddSeconds(CustomGameOptions.InitialCooldowns-CustomGameOptions.DouseCd);
            }
        }
    }
}