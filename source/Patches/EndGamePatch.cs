  
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Hazel;
using UnhollowerBaseLib;
using System;
using System.Text;

namespace TownOfUs.Patches {

    static class AdditionalTempData {
        public static List<PlayerRoleInfo> playerRoles = new List<PlayerRoleInfo>();

        public static void clear() {
            playerRoles.Clear();
        }

        internal class PlayerRoleInfo {
            public string PlayerName { get; set; }
            public string Role {get;set;}
        }
    }


    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class OnGameEndPatch {

        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)]ref GameOverReason reason, [HarmonyArgument(1)]bool showAd) {
            AdditionalTempData.clear();
            var playerRole = "";
            // Theres a better way of doing this e.g. switch statement or dictionary. But this works for now.
            foreach(var playerControl in PlayerControl.AllPlayerControls) {
                if (playerControl.Is(RoleEnum.Crewmate)) {playerRole = "<color=#FFFFFFFF>Crewmate</color>";}
                else if (playerControl.Is(RoleEnum.Impostor)) {playerRole = "<color=#FF0000FF>Impostor</color>";}
                else if (playerControl.Is(RoleEnum.LoverImpostor)) {playerRole = "<color=#FF0000FF>Loving Impostor</color>";}
                else if (playerControl.Is(RoleEnum.Altruist)) {playerRole = "<color=#660000FF>Altruist</color>";}
                else if (playerControl.Is(RoleEnum.Chameleon)) {playerRole = "<color=#00FF00FF>Chameleon</color>";}
                else if (playerControl.Is(RoleEnum.Engineer)) {playerRole = "<color=#FFA60AFF>Engineer</color>";}
                else if (playerControl.Is(RoleEnum.Investigator)) {playerRole = "<color=#00B3B3FF>Investigator</color>";}
                else if (playerControl.Is(RoleEnum.Lover)) {playerRole = "<color=#FF66CCFF>Lover</color>";}
                else if (playerControl.Is(RoleEnum.Mayor)) {playerRole = "<color=#704FA8FF>Mayor</color>";}
                else if (playerControl.Is(RoleEnum.Medic)) {playerRole = "<color=#006600FF>Medic</color>";}
                else if (playerControl.Is(RoleEnum.Sheriff)) {playerRole = "<color=#FFFF00FF>Sheriff</color>";}
                else if (playerControl.Is(RoleEnum.Swapper)) {playerRole = "<color=#66E666FF>Swapper</color>";}
                else if (playerControl.Is(RoleEnum.TimeLord)) {playerRole = "<color=#0000FFFF>Time Lord</color>";}
                else if (playerControl.Is(RoleEnum.Tracker)) {playerRole = "<color=#00B3B3FF>Tracker</color>";}
                else if (playerControl.Is(RoleEnum.Seer)) {playerRole = "<color=#FFCC80FF>Seer</color>";}
                else if (playerControl.Is(RoleEnum.Snitch)) {playerRole = "<color=#D4AF37FF>Snitch</color>";}
                else if (playerControl.Is(RoleEnum.Spy)) {playerRole = "<color=#CCA3CCFF>Spy</color>";}
                else if (playerControl.Is(RoleEnum.Vigilante)) {playerRole = "<color=#FFFF00FF>Vigilante</color>";}
                else if (playerControl.Is(RoleEnum.Arsonist)) {playerRole = "<color=#FF4D00FF>Arsonist</color>";}
                else if (playerControl.Is(RoleEnum.Executioner)) {playerRole = "<color=#8C4005FF>Executioner</color>";}
                else if (playerControl.Is(RoleEnum.Glitch)) {playerRole = "<color=#00FF00FF>Glitch</color>";}
                else if (playerControl.Is(RoleEnum.Jester)) {playerRole = "<color=#FFBFCCFF>Jester</color>";}
                else if (playerControl.Is(RoleEnum.Shifter)) {playerRole = "<color=#999999FF>Shifter</color>";}
                else if (playerControl.Is(RoleEnum.Cannibal)) {playerRole = "<color=#AC8A00FF>Cannibal</color>";}
                else if (playerControl.Is(RoleEnum.Phantom)) {playerRole = "<color=#662962>Phantom</color>";}
                else if (playerControl.Is(RoleEnum.Assassin)) {playerRole = "<color=#FF0000FF>Assassin</color>";}
                else if (playerControl.Is(RoleEnum.Camouflager)) {playerRole = "<color=#FF0000FF>Camouflager</color>";}
                else if (playerControl.Is(RoleEnum.Grenadier)) {playerRole = "<color=#FF0000FF>Grenadier</color>";}
                else if (playerControl.Is(RoleEnum.Janitor)) {playerRole = "<color=#FF0000FF>Janitor</color>";}
                else if (playerControl.Is(RoleEnum.Miner)) {playerRole = "<color=#FF0000FF>Miner</color>";}
                else if (playerControl.Is(RoleEnum.Morphling)) {playerRole = "<color=#FF0000FF>Morphling</color>";}
                else if (playerControl.Is(RoleEnum.Poisoner)) {playerRole = "<color=#FF0000FF>Poisoner</color>";}
                else if (playerControl.Is(RoleEnum.Swooper)) {playerRole = "<color=#FF0000FF>Swooper</color>";}
                else if (playerControl.Is(RoleEnum.Underdog)) {playerRole = "<color=#FF0000FF>Underdog</color>";}
                else if (playerControl.Is(RoleEnum.Undertaker)) {playerRole = "<color=#FF0000FF>Undertaker</color>";}
                if (playerControl.Is(ModifierEnum.Bait)) {
                    playerRole += " (<color=#00B3B3FF>Bait</color>)";
                } else if (playerControl.Is(ModifierEnum.BigBoi)) {
                    playerRole += " (<color=#D4AF37FF>Giant</color>)";
                } else if (playerControl.Is(ModifierEnum.Blind)) {
                    playerRole += " (<color=#AAAAAAFF>Blind</color>)";
                } else if (playerControl.Is(ModifierEnum.ButtonBarry)) {
                    playerRole += " (<color=#E600FFFF>Button Barry</color>)";
                } else if (playerControl.Is(ModifierEnum.Child)) {
                    playerRole += " (<color=#FFFFFFFF>Child</color>)";
                } else if (playerControl.Is(ModifierEnum.Diseased)) {
                    playerRole += " (<color=#808080FF>Diseased</color>)";
                } else if (playerControl.Is(ModifierEnum.Drunk)) {
                    playerRole += " (<color=#758000FF>Drunk</color>)";
                } else if (playerControl.Is(ModifierEnum.Flash)) {
                    playerRole += " (<color=#FF8080FF>Flash</color>)";
                } else if (playerControl.Is(ModifierEnum.Tiebreaker)) {
                    playerRole += " (<color=#99E699FF>Tiebreaker</color>)";
                } else if (playerControl.Is(ModifierEnum.Torch)) {
                    playerRole += " (<color=#FFFF99FF>Torch</color>)";
                } else if (playerControl.Is(ModifierEnum.Volatile)) {
                    playerRole += " (<color=#FFA60AFF>Volatile</color>)";
                }  
                AdditionalTempData.playerRoles.Add(new AdditionalTempData.PlayerRoleInfo() { PlayerName = playerControl.Data.PlayerName, Role = playerRole });
            }
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public class EndGameManagerSetUpPatch {
        public static void Postfix(EndGameManager __instance) {
            GameObject bonusText = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
            bonusText.transform.position = new Vector3(__instance.WinText.transform.position.x, __instance.WinText.transform.position.y - 0.8f, __instance.WinText.transform.position.z);
            bonusText.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            TMPro.TMP_Text textRenderer = bonusText.GetComponent<TMPro.TMP_Text>();
            textRenderer.text = "";

            if (CustomGameOptions.ShowEndgameSummary) {
                var position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane));
                GameObject roleSummary = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
                roleSummary.transform.position = new Vector3(__instance.ExitButton.transform.position.x + 0.1f, position.y - 0.1f, -14f); 
                roleSummary.transform.localScale = new Vector3(1f, 1f, 1f);

                var roleSummaryText = new StringBuilder();
                roleSummaryText.AppendLine("End game summary:");
                foreach(var data in AdditionalTempData.playerRoles) {
                    var role = string.Join(" ", data.Role);
                    roleSummaryText.AppendLine($"{data.PlayerName} - {role}");
                }
                TMPro.TMP_Text roleSummaryTextMesh = roleSummary.GetComponent<TMPro.TMP_Text>();
                roleSummaryTextMesh.alignment = TMPro.TextAlignmentOptions.TopLeft;
                roleSummaryTextMesh.color = Color.white;
                roleSummaryTextMesh.fontSizeMin = 1.5f;
                roleSummaryTextMesh.fontSizeMax = 1.5f;
                roleSummaryTextMesh.fontSize = 1.5f;
                
                var roleSummaryTextMeshRectTransform = roleSummaryTextMesh.GetComponent<RectTransform>();
                roleSummaryTextMeshRectTransform.anchoredPosition = new Vector2(position.x + 3.5f, position.y - 0.1f);
                roleSummaryTextMesh.text = roleSummaryText.ToString();
            }
            AdditionalTempData.clear();
        }
    }
}