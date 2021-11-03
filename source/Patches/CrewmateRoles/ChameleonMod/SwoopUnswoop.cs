using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.CrewmateRoles.ChameleonMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    [HarmonyPriority(Priority.Last)]
    public class SwoopUnswoop
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Chameleon))
            {
                var swooper = (Chameleon) role;
                if (swooper.IsSwooped)
                    swooper.Swoop();
                else if (swooper.Enabled) swooper.UnSwoop();
            }
        }
    }
}