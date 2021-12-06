using HarmonyLib;
using UnityEngine;

namespace TownOfUs {
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public static class LogoPatch
    {
        private static Sprite Sprite => TownOfUs.LogoBanner;
        static void Postfix(PingTracker __instance) {
            var amongUsLogo = GameObject.Find("bannerLogo_AmongUs");
            if (amongUsLogo != null) {
                amongUsLogo.transform.localScale *= 0.6f;
                amongUsLogo.transform.position += Vector3.up * 0.25f;
            }

            var torLogo = new GameObject("bannerLogo_TownOfH");
            torLogo.transform.position = Vector3.up;
            var renderer = torLogo.AddComponent<SpriteRenderer>();
            renderer.sprite = Sprite;
        }
    }
}