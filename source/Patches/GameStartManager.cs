/*using System.Collections;
using System.Linq;
using HarmonyLib;
using Reactor;
using TownOfUs.Extensions;
using UnityEngine;
using UnhollowerBaseLib;
using System;

namespace TownOfUs.Patches {
    public class GameStartManagerPatch {
        public static GameObject TownOfHLobbyBanner;

        [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
        public class GameStartManagerUpdatePatch {
            public static void Prefix(GameStartManager __instance) {
                if (TownOfHLobbyBanner == null) {
                    return;
                }
            }
        }

        [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.Start))]
        public static class RearrangeLobby {
            public static void Postfix(LobbyBehaviour __instance) {
                Camera main = Camera.main;
                FollowerCamera component = main.GetComponent<FollowerCamera>();
                if (component)
                {
                    component.shakeAmount = 0f;
                    component.shakePeriod = 0;
                }

                TownOfHLobbyBanner = new GameObject("TownOfHLobbyBanner");
                Vector3 position = new Vector3(0f, 4.5f, 7);
                TownOfHLobbyBanner.transform.position = position;
                TownOfHLobbyBanner.transform.localScale = new Vector3(1, 1, 1);

                var panelRenderer = TownOfHLobbyBanner.AddComponent<SpriteRenderer>();
                if (DateTime.Today.Month == 8 && DateTime.Today.Day == 7) {panelRenderer.sprite = TownOfUs.BirthdayCtri;}
                else if (DateTime.Today.Month == 5 && DateTime.Today.Day == 26) {panelRenderer.sprite = TownOfUs.BirthdayChaos;}
                else if (DateTime.Today.Month == 10 && DateTime.Today.Day == 9) {panelRenderer.sprite = TownOfUs.BirthdayBal;}
                else if (DateTime.Today.Month == 6 && DateTime.Today.Day == 22) {panelRenderer.sprite = TownOfUs.BirthdayH;}
                else if (DateTime.Today.Month == 12 && DateTime.Today.Day > 15 && DateTime.Today.Day < 31) {panelRenderer.sprite = TownOfUs.ChristmasLobby;}
                else {panelRenderer.sprite = TownOfUs.LogoLobbyBanner;}

                TownOfHLobbyBanner.gameObject.transform.SetParent(__instance.transform);
            }
        }
    }
}*/