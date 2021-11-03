using System.Linq;
using HarmonyLib;
using Reactor.Extensions;
using TownOfUs.Extensions;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.TrackerMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class UpdateTrackerArrows
    {
        private static float _time = 0f;
        private static float Interval => CustomGameOptions.TrackerInterval;
        public static void Postfix(PlayerControl __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Tracker))
            {
                var tracker = (Tracker) role;
                if (PlayerControl.LocalPlayer.Data.IsDead || tracker.Player.Data.IsDead)
                {
                    tracker.TrackerArrows.DestroyAll();
                    tracker.TrackerArrows.Clear();
                    return;
                } else {
                    _time += Time.deltaTime;
                    if (_time >= Interval)
                    {
                        _time -= Interval;
                        foreach (var (arrow, target) in Utils.Zip(tracker.TrackerArrows, tracker.TrackerTargets))
                        {
                            if (target.Data.IsDead)
                            {
                                arrow.Destroy();
                                if (arrow.gameObject != null) arrow.gameObject.Destroy();
                            } else {
                                arrow.target = target.transform.position;
                            }
                        }
                    }
                }
            }
        }
    }
}