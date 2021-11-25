using HarmonyLib;
using Hazel;
using TownOfUs.Roles;

namespace TownOfUs.CrewmateRoles.ChameleonMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class PerformKill
    {
        public static bool Prefix(KillButtonManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Chameleon)) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Chameleon>(PlayerControl.LocalPlayer);
            if (__instance == role.SwoopButton)
            {
                if (__instance.isCoolingDown) return false;
                if (!__instance.isActiveAndEnabled) return false;
                if (role.SwoopTimer() != 0) return false;

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.ChameleonSwoop, SendOption.Reliable, -1);
                var position = PlayerControl.LocalPlayer.transform.position;
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                role.TimeRemaining = CustomGameOptions.ChameleonDuration;
                role.Swoop();
                return false;
            }

            return true;
        }
    }
}