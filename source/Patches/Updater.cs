using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnhollowerBaseLib;
using System;
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
    public class ModUpdaterButton {
        private static void Prefix(MainMenuManager __instance) {
            ModUpdater.LaunchUpdater();
            if (!ModUpdater.hasUpdate) return;
            var template = GameObject.Find("ExitGameButton");
            if (template == null) return;

            TwitchManager man = DestroyableSingleton<TwitchManager>.Instance;
            ModUpdater.InfoPopup = UnityEngine.Object.Instantiate<GenericPopup>(man.TwitchPopup);
            ModUpdater.InfoPopup.TextAreaTMP.fontSize *= 0.7f;
            ModUpdater.InfoPopup.TextAreaTMP.enableAutoSizing = false;

            ModUpdater.ExecuteUpdate();
        }
    }

    public class ModUpdater { 
        public static bool running = false;
        public static bool hasUpdate = false;
        public static string updateURI = null;
        private static Task updateTask = null;
        public static GenericPopup InfoPopup;

        public static void LaunchUpdater() {
            if (running) return;
            running = true;
            checkForUpdate().GetAwaiter().GetResult();
            clearOldVersions();
            updateRegions().GetAwaiter().GetResult();;
        }

        public static void ExecuteUpdate() {
            string info = "Updating Town Of Us -H\nPlease wait...";
            ModUpdater.InfoPopup.Show(info);
            if (updateTask == null) {
                if (updateURI != null) {
                    updateTask = downloadUpdate();
                } else {
                    info = "Unable to auto-update\nPlease update manually";
                }
            } else {
                info = "Update might already\nbe in progress";
            }
            ModUpdater.InfoPopup.StartCoroutine(Effects.Lerp(0.01f, new System.Action<float>((p) => { ModUpdater.setPopupText(info); })));
        }
        
        public static void clearOldVersions() {
            try {
                DirectoryInfo d = new DirectoryInfo(Path.GetDirectoryName(Application.dataPath) + @"\BepInEx\plugins");
                string[] files = d.GetFiles("*.old").Select(x => x.FullName).ToArray();
                foreach (string f in files)
                    File.Delete(f);
            } catch (System.Exception e) {
                System.Console.WriteLine("Exception occured when clearing old versions:\n" + e);
            }
        }

        public static async Task<bool> updateRegions() {
            try{
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Add("User-Agent", "TownOfUs Updater");
                var response = await http.GetAsync(new System.Uri("https://www.dropbox.com/s/egmssmvqnev2ul9/regionInfo.json?dl=1"), HttpCompletionOption.ResponseContentRead);
                if (response.StatusCode != HttpStatusCode.OK || response.Content == null) {
                    return false;
                }
                string LocalLowPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Replace("Roaming","LocalLow");
                string fullname = LocalLowPath + "\\Innersloth\\Among Us\\regionInfo.json";
                if (File.Exists(fullname + ".old")) // Clear old file in case it wasnt;
                    File.Delete(fullname + ".old");

                File.Move(fullname, fullname + ".old"); // rename current region file to old

                using (var responseStream = await response.Content.ReadAsStreamAsync()) {
                    using (var fileStream = File.Create(fullname)) {
                        responseStream.CopyTo(fileStream);
                        return true;
                    }
                }
            } catch (System.Exception e) {
                System.Console.WriteLine("Exception occured when updating regions:\n" + e);
                return false;
            }
        }

        public static async Task<bool> checkForUpdate() {
            try {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Add("User-Agent", "TownOfUs Updater");
                var response = await http.GetAsync(new System.Uri("https://api.github.com/repos/ItsTheNumberH/Town-Of-H/releases/latest"), HttpCompletionOption.ResponseContentRead);
                if (response.StatusCode != HttpStatusCode.OK || response.Content == null) {
                    System.Console.WriteLine("Server returned no data: " + response.StatusCode.ToString());
                    return false;
                }
                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                
                string tagname = data["tag_name"]?.ToString();
                if (tagname == null) {
                    return false; // Something went wrong
                }
                // check version
                System.Version ver = System.Version.Parse(tagname.Replace("v", ""));
                int diff = TownOfUs.Version.CompareTo(ver);
                if (diff < 0) { // Update required
                    hasUpdate = true;
                    JToken assets = data["assets"];
                    if (!assets.HasValues)
                        return false;

                    for (JToken current = assets.First; current != null; current = current.Next) {
                        string browser_download_url = current["browser_download_url"]?.ToString();
                        if (browser_download_url != null && current["content_type"] != null) {
                            if (browser_download_url.EndsWith(".dll")) {
                                updateURI = browser_download_url;
                                return true;
                            }
                        }
                    }
                }  
            } catch (System.Exception ex) {
                System.Console.WriteLine(ex);
            }
            return false;
        }

        public static async Task<bool> downloadUpdate() {
            try {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Add("User-Agent", "TownOfUs Updater");
                var response = await http.GetAsync(new System.Uri(updateURI), HttpCompletionOption.ResponseContentRead);
                if (response.StatusCode != HttpStatusCode.OK || response.Content == null) {
                    System.Console.WriteLine("Server returned no data: " + response.StatusCode.ToString());
                    return false;
                }
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                System.UriBuilder uri = new System.UriBuilder(codeBase);
                string fullname = System.Uri.UnescapeDataString(uri.Path);
                if (File.Exists(fullname + ".old")) // Clear old file in case it wasnt;
                    File.Delete(fullname + ".old");

                File.Move(fullname, fullname + ".old"); // rename current executable to old

                using (var responseStream = await response.Content.ReadAsStreamAsync()) {
                    using (var fileStream = File.Create(fullname)) { // probably want to have proper name here
                        responseStream.CopyTo(fileStream);
                    }
                }
                showPopup("Town Of Us -H\nupdated successfully.\nPlease RESTART the game.");
                return true;
            } catch (System.Exception ex) {
                System.Console.WriteLine(ex);
            }
            showPopup("Update wasn't successful\nTry again later,\nor update manually.");
            return false;
        }
        private static void showPopup(string message) {
            setPopupText(message);
            InfoPopup.gameObject.SetActive(true);
        }

        public static void setPopupText(string message) {
            if (InfoPopup == null)
                return;
            if (InfoPopup.TextAreaTMP != null) {
                InfoPopup.TextAreaTMP.text = message;
            }
        }
    }
}