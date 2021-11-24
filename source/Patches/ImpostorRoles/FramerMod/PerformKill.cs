using System;
using HarmonyLib;
using Hazel;
using TownOfUs.CrewmateRoles.MedicMod;
using TownOfUs.Roles;

namespace TownOfUs.Patches.ImpostorRoles.FramerMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class PerformKill
    {
        public static bool Prefix(KillButtonManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Framer))
            {
                return true;
            }

            if (
                !PlayerControl.LocalPlayer.CanMove
                || PlayerControl.LocalPlayer.Data.IsDead
                )
            {
                return false;
            }

            Framer role = Role.GetRole<Framer>(PlayerControl.LocalPlayer);
            if (__instance != role.FrameButton)
            {
                return true;
            }

            if (
                __instance.isCoolingDown
                || !__instance.isActiveAndEnabled
                || role.FrameTimer() != 0
                || role.Target == null
                || role.Target.Data.IsImpostor
            )
            {
                return false;
            }

            if (role.Target.isShielded())
            {
                if (__instance.isActiveAndEnabled && !__instance.isCoolingDown)
                {
                    var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte) CustomRPC.AttemptSound, SendOption.Reliable, -1);
                    writer2.Write(role.Target.getMedic().Player.PlayerId);
                    writer2.Write(role.Target.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer2);

                    System.Console.WriteLine(CustomGameOptions.ShieldBreaks + "- shield break");
                    if (CustomGameOptions.ShieldBreaks)
                        PlayerControl.LocalPlayer.SetKillTimer(PlayerControl.GameOptions.KillCooldown);

                    StopKill.BreakShield(role.Target.getMedic().Player.PlayerId, role.Target.PlayerId, CustomGameOptions.ShieldBreaks);
                }
                return false;
            }


            // Sets framed player
            role.StartFrame(role.Target);

            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.Frame,
                SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(role.Framed.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            return false;
        }
    }
}
