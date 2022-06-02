using HarmonyLib;
using Hazel;
using TownOfUs.Roles;
using TownOfUs.CrewmateRoles.MedicMod;

namespace TownOfUs.ImpostorRoles.BlackmailerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Blackmailer)) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Blackmailer>(PlayerControl.LocalPlayer);
            var target = role.ClosestPlayer;
            if (__instance == role.BlackmailButton)
            {
                if (!__instance.isActiveAndEnabled || role.ClosestPlayer == null) return false;

                if (role.ClosestPlayer.IsOnAlert())
                {
                    if (role.Player.IsShielded())
                    {
                        var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                        writer2.Write(PlayerControl.LocalPlayer.GetMedic().Player.PlayerId);
                        writer2.Write(PlayerControl.LocalPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer2);

                        StopKill.BreakShield(PlayerControl.LocalPlayer.GetMedic().Player.PlayerId, PlayerControl.LocalPlayer.PlayerId, CustomGameOptions.ShieldBreaks);
                    }
                    else
                    {
                        Utils.RpcMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
                    }

                    return false;
                }
                if (__instance.isCoolingDown) return false;
                if (!__instance.isActiveAndEnabled) return false;
                if (role.BlackmailTimer() != 0) return false;
                role.Blackmailed?.MyRend.material.SetFloat("_Outline", 0f);
                role.Blackmailed = target;
                role.BlackmailButton.SetCoolDown(1f, 1f);
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.Blackmail, SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write(target.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                return false;
            }
            return true;
        }
    }
}