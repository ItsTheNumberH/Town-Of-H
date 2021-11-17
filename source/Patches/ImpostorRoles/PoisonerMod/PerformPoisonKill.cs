using System;
using HarmonyLib;
using Hazel;
using TownOfUs.Roles;
using UnityEngine;
using Reactor;
using TownOfUs.CrewmateRoles.MedicMod;

namespace TownOfUs.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class PerformPoisonKill
    {
        public static Sprite PoisonSprite => TownOfUs.PoisonSprite;
        public static Sprite PoisonedSprite => TownOfUs.PoisonedSprite;

        public static bool Prefix(KillButtonManager __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner);
            if (!flag) return true;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
            var target = role.ClosestPlayer;
            if (target == null) return false;
            if (!__instance.isActiveAndEnabled) return false;
            if (role.PoisonTimer() > 0) return false;
            if (role.ClosestPlayer.isShielded())
            {
                var medic = role.ClosestPlayer.getMedic().Player.PlayerId;
                var writer1 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.AttemptSound, SendOption.Reliable, -1);
                writer1.Write(medic);
                writer1.Write(role.ClosestPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer1);

                if (CustomGameOptions.ShieldBreaks) role.LastPoisoned = DateTime.UtcNow;

                StopKill.BreakShield(medic, role.ClosestPlayer.PlayerId, CustomGameOptions.ShieldBreaks);

                return false;
            }
            role.PoisonedPlayer = target;
            role.PoisonButton.SetTarget(null);
            DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);

            role.TimeRemaining = CustomGameOptions.PoisonerDuration;
            role.PoisonButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.PoisonerDuration);
            role.Player.SetKillTimer(0);
            return false;
        }
    }
}
