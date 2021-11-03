using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.ImpostorRoles.UnderdogMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public class PerformKill
    {
        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            var role = Role.GetRole(__instance);
            if (role?.RoleType == RoleEnum.Underdog)
                ((Underdog)role).SetKillTimer();
        }

        internal static bool LastImp()
        {
            return PlayerControl.AllPlayerControls.ToArray()
                .Count(x => x.Data.IsImpostor && !x.Data.IsDead) == 1;
        }
        internal static float LastImpRemainingPlayers()
        {
            var totalalive = PlayerControl.AllPlayerControls.ToArray()
                .Count(x => !x.Data.IsImpostor && !x.Data.IsDead);
            try {
                if (totalalive > 5) {
                    return 1.5f;
                } else if (totalalive == 5) {
                    return 1.2f;
                } else if (totalalive == 4) {
                    return 1f;
                } else if (totalalive == 3) {
                    return 0.75f;
                } else if (totalalive == 2) {
                    return 0.5f;
                } else {
                    return 0.25f;
                }
            } catch {
                return 1f;
            }
        }
    }
}
