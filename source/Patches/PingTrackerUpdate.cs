using HarmonyLib;
using UnityEngine;

namespace TownOfUs
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {

        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();

            __instance.text.text =
                "<color=#00FF00FF>Town Of Us -H " + TownOfUs.VersionString + "</color>\n" +
                $"Ping: {AmongUsClient.Instance.Ping}ms\n";
        }
    }
}
