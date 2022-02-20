using HarmonyLib;

namespace TownOfUs.NeutralRoles.ArsonistMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetRole))]
    public class NoButtons
    {
        public static void Postfix()
        {
            if (!CustomGameOptions.ArsonistButton)
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist)) PlayerControl.LocalPlayer.RemainingEmergencies = 0;
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Start))]
    public class NoButtonsHost
    {
        public static void Postfix()
        {
            if (!CustomGameOptions.ArsonistButton) 
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Arsonist)) PlayerControl.LocalPlayer.RemainingEmergencies = 0;
        }
    }
}