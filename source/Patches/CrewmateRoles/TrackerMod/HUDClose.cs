using System;
using HarmonyLib;
using TownOfUs.Roles;
using Object = UnityEngine.Object;

namespace TownOfUs.CrewmateRoles.TrackerMod
{
    public enum TrackPer
    {
        Round,
        Game
    }
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            foreach (var role in Role.GetRoles(RoleEnum.Tracker))
            {
                var tracker = (Tracker) role;
                tracker.LastTracked = DateTime.UtcNow;
                tracker.LastTracked = tracker.LastTracked.AddSeconds(-10.0);
                if (CustomGameOptions.TrackPer == TrackPer.Round) tracker.UsedTrack = false;
            }
        }
    }
}