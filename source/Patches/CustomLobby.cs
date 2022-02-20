using HarmonyLib;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using Hazel;
using System;
using UnhollowerBaseLib;

namespace TownOfUs.Patches
{
    public static class LobbyBehaviour_Patch
    {
        private static GameObject prefab = null;
        private static GameObject getOldLobby(string name = "Lobby(Clone)") {

            GameObject lobby = GameObject.Find(name);
            if (lobby == null) {
                throw new Exception("Lobby not found");
            }
            return lobby;
        }

        private static void LoadPrefab() {

            prefab = CustomMain.customAssets.customLobby;
            if (prefab == null) {
                throw new Exception("Loading asset error");
            }

            Material shadowShader = null;
            GameObject background = GameObject.Find("Lobby(Clone)/Background");
            {
                SpriteRenderer sp = background.GetComponent<SpriteRenderer>();
                if (sp != null) {
                    shadowShader = sp.material;
                }
            }
            {
                SpriteRenderer sp = prefab.GetComponent<SpriteRenderer>();
                if (sp != null && shadowShader != null) {
                    sp.material = shadowShader;
                }
                else {
                    throw new Exception("shadowShader not found");
                }
            }

        }
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

        class LobbyBehavour_Start_Patch
        {
            public static void Postfix(LobbyBehaviour __instance) {
                if (prefab == null) {
                    LoadPrefab();
                }

                GameObject instance = GameObject.Instantiate(prefab);
                instance.transform.position = new Vector3(0f, 0.85f, 0f);


                GameObject oldLobby = getOldLobby();
                oldLobby.GetComponent<Collider2D>().enabled = false;
                GameObject.Find("Lobby(Clone)/Background").SetActive(false);

                FollowerCamera component = Camera.main.GetComponent<FollowerCamera>();
                if (component)
                {
                    component.shakeAmount = 0f;
                    component.shakePeriod = 0;
                }

                TownOfHLobbyBanner = new GameObject("TownOfHLobbyBanner");
                Vector3 position = new Vector3(0f, 4.5f, -1);
                TownOfHLobbyBanner.transform.position = position;
                TownOfHLobbyBanner.transform.localScale = new Vector3(1, 1, 1);

                var panelRenderer = TownOfHLobbyBanner.AddComponent<SpriteRenderer>();
                if (DateTime.Today.Month == 8 && DateTime.Today.Day == 7) {panelRenderer.sprite = TownOfUs.BirthdayCtri;}
                else if (DateTime.Today.Month == 5 && DateTime.Today.Day == 26) {panelRenderer.sprite = TownOfUs.BirthdayChaos;}
                else if (DateTime.Today.Month == 10 && DateTime.Today.Day == 9) {panelRenderer.sprite = TownOfUs.BirthdayBal;}
                else if (DateTime.Today.Month == 6 && DateTime.Today.Day == 22) {panelRenderer.sprite = TownOfUs.BirthdayH;}
                else if (DateTime.Today.Month == 12 && DateTime.Today.Day > 15 && DateTime.Today.Day < 31) {panelRenderer.sprite = TownOfUs.ChristmasLobby;}
                else {panelRenderer.sprite = TownOfUs.LogoLobbyBanner;}

                TownOfHLobbyBanner.gameObject.transform.SetParent(instance.transform);
            }
        }

        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.Start))]
        class GameStartPatch
        {
            // Deactivate custom lobby items on game start
            public static void Prefix(ShipStatus __instance) {
                try {
                    if (!DestroyableSingleton<TutorialManager>.InstanceExists) {
                        GameObject allulbackground = GameObject.Find("allul_customLobby(Clone)");
                        allulbackground.SetActive(false);
                    }
                } catch {
                    
                }
            }
        }
    }
}
