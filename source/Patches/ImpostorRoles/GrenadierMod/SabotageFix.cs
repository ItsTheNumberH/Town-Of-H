using Hazel;

namespace TownOfUs.ImpostorRoles.GrenadierMod
{
    public class SabotageFix
    {

        public static bool FixComms()
        {
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 0);
            return false;
        }

        public static bool FixMiraComms()
        {
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 0);
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 1);
            return false;
        }

        public static bool FixAirshipReactor()
        {
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16 | 0);
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16 | 1);
            return false;
        }

        public static bool FixReactor(SystemTypes system)
        {
            ShipStatus.Instance.RpcRepairSystem(system, 16);
            return false;
        }

        public static bool FixOxygen()
        {
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 16);
            return false;
        }

        public static bool FixLights(SwitchSystem lights)
        {
            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.FixLights, SendOption.Reliable, -1);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            lights.ActualSwitches = lights.ExpectedSwitches;

            return false;
        }
    }
}