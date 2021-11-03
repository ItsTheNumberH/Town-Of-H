using System;
using HarmonyLib;
using Hazel;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.TrackerMod
{
    [HarmonyPatch(typeof(KillButtonManager), nameof(KillButtonManager.PerformKill))]
    public class PerformKill
    {
        public static Sprite Sprite => TownOfUs.Arrow;
        public static bool Prefix(KillButtonManager __instance)
        {
            if (__instance != DestroyableSingleton<HudManager>.Instance.KillButton) return true;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tracker)) return true;
            var role = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
            if (role.UsedTrack) return false;
            if (!PlayerControl.LocalPlayer.CanMove || role.ClosestPlayer == null) return false;
            var flag2 = role.TrackerTimer() == 0f;
            if (!flag2) return false;
            if (!__instance.enabled) return false;
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            if (Vector2.Distance(role.ClosestPlayer.GetTruePosition(),
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            if (role.ClosestPlayer == null) return false;
            var playerId = role.ClosestPlayer.PlayerId;
            role.UsedTrack = true;

            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte) CustomRPC.Track, SendOption.Reliable, -1);
            writer.Write(PlayerControl.LocalPlayer.PlayerId);
            writer.Write(playerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

            role.Tracked.Add(role.ClosestPlayer.PlayerId);
            role.LastTracked = DateTime.UtcNow;

            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (!role.Tracked.Contains(player.PlayerId)) continue;
                var gameObj = new GameObject();
                var arrow = gameObj.AddComponent<ArrowBehaviour>();
                gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                var renderer = gameObj.AddComponent<SpriteRenderer>();
                renderer.sprite = Sprite;
                if (RainbowUtils.IsRainbow(player.Data.ColorId)) {
                    renderer.color = RainbowUtils.Rainbow;
                } else {
                    renderer.color = Palette.PlayerColors[player.Data.ColorId];
                }
                arrow.image = renderer;
                gameObj.layer = 5;
                role.TrackerArrows.Add(arrow);
                role.TrackerTargets.Add(player);
            }
            return false;
        }
    }
}
