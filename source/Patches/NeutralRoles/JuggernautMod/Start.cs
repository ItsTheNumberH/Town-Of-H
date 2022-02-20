using System;
using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.NeutralRoles.JuggernautMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__18), nameof(IntroCutscene._CoBegin_d__18.MoveNext))]
    internal class Start
    {
        private static void Postfix(IntroCutscene._CoBegin_d__18 __instance)
        {
            var juggernaut = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Juggernaut);
            if (juggernaut != null)
            {
                ((Juggernaut)juggernaut).LastKill = DateTime.UtcNow.AddSeconds(CustomGameOptions.InitialCooldowns + (CustomGameOptions.GlitchKillCooldown + 5.0f) * -1);
            }
        }
    }
}