using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using Il2CppSystem;
using Hazel;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnhollowerBaseLib;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Twitch;

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