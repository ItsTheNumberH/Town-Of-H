using HarmonyLib;
using Hazel;
using Reactor;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class PerformKillButton

    {
        public static bool Prefix(KillButtonManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal)) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Cannibal>(PlayerControl.LocalPlayer);

            if (__instance == role.EatButton)
            {
                if (!__instance.enabled) return false;
                var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
                if (Vector2.Distance(role.CurrentTarget.TruePosition,
                    PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
                var playerId = role.CurrentTarget.ParentId;

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.CannibalEat, SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write(playerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                Coroutines.Start(Coroutine.EatCoroutine(role.CurrentTarget, role));

                role.bodiesEaten += 1;

                if (role.bodiesEaten >= CustomGameOptions.NumberCannibalBodies) {
                    ((Cannibal) role).Wins();
                    var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                        (byte) CustomRPC.CannibalWin, SendOption.Reliable, -1);
                    writer2.Write(role.Player.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer2);
                    Utils.EndGame();
                }
                return false;
            }
            return true;
        }
    }
}