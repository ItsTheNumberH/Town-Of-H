using System;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.ImpostorRoles.FramerMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__14), nameof(IntroCutscene._CoBegin_d__14.MoveNext))]
    public static class Start
    {
        public static void Postfix(IntroCutscene._CoBegin_d__14 __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Framer))
            {
                var framer = (Framer) role;
                framer.LastFramed = DateTime.UtcNow;
                framer.LastFramed = framer.LastFramed.AddSeconds(CustomGameOptions.InitialCooldowns - CustomGameOptions.FrameCooldown);
            }
        }
    }
}