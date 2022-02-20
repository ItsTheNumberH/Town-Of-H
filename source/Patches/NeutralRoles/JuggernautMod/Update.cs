using System.Linq;
using HarmonyLib;
using InnerNet;
using TownOfUs.Roles;

namespace TownOfUs.NeutralRoles.JuggernautMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    internal class Update
    {
        private static void Postfix(HudManager __instance)
        {
            var juggernaut = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Juggernaut);
            if (AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started)
                if (juggernaut != null)
                    if (PlayerControl.LocalPlayer.Is(RoleEnum.Juggernaut))
                        Role.GetRole<Juggernaut>(PlayerControl.LocalPlayer).Update(__instance);
        }
    }
}