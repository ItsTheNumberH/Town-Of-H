using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.Patches.ImpostorRoles.FramerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class FrameUpdate
    {
        public static void Postfix(HudManager __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Framer))
            {
                Framer framer = (Framer) role;
                framer.FrameTick();
            }
        }
    }
}
