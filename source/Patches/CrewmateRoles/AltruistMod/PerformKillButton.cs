using HarmonyLib;
using Hazel;
using Reactor;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.AltruistMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class PerformKillButton
    {
        public static bool Prefix(KillButtonManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Altruist)) return true;
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Altruist>(PlayerControl.LocalPlayer);

            if (__instance.isCoolingDown) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            if (role == null)
                return false;
            if (role.CurrentTarget == null)
                return false;
            if (Vector2.Distance(role.CurrentTarget.TruePosition,
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            var playerId = role.CurrentTarget.ParentId;

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.AltruistRevive, SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(playerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            Coroutines.Start(Coroutine.AltruistRevive(role.CurrentTarget, role));
            return false;
        }
    }
}