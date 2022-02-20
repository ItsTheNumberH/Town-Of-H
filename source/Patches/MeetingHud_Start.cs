using HarmonyLib;
using Object = UnityEngine.Object;
using System.Linq;
using UnityEngine;

namespace TownOfUs
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MeetingHud_Start
    {
        public static void Postfix(MeetingHud __instance)
        {
            Utils.ShowDeadBodies = PlayerControl.LocalPlayer.Data.IsDead;
        }
    }
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Close))]
    public class MeetingHud_Close
    {
        public static void Postfix(MeetingHud __instance)
        {
            var deadBodies = Object.FindObjectsOfType<DeadBody>();
            foreach (var body in deadBodies) {
                Object.Destroy(body.gameObject);
            }
        }
    }
}