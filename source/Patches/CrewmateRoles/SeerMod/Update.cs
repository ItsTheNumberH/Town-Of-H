using System.Linq;
using HarmonyLib;
using TownOfUs.ImpostorRoles.CamouflageMod;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.SeerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class Update
    {
        public static string NameText(PlayerControl player, string str = "", bool meeting = false)
        {
            if (CamouflageUnCamouflage.IsCamoed)
            {
                if (meeting && !CustomGameOptions.MeetingColourblind) return player.name + str;
                return "";
            }

            return player.name + str;
        }

        private static void UpdateMeeting(MeetingHud __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Seer))
            {
                var seerRole = (Seer) role;
                if (!seerRole.Investigated.Contains(PlayerControl.LocalPlayer.PlayerId)) continue;
                if (!seerRole.CheckSeeReveal(PlayerControl.LocalPlayer)) continue;
                var state = __instance.playerStates.FirstOrDefault(x => x.TargetPlayerId == seerRole.Player.PlayerId);
                state.NameText.color = seerRole.Color;
                state.NameText.text = NameText(seerRole.Player, " (Seer)", true);
            }
        }

        private static void UpdateMeeting(MeetingHud __instance, Seer seer)
        {
            if (!PlayerControl.LocalPlayer.Data.IsDead) {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!seer.Investigated.Contains(player.PlayerId)) continue;
                    foreach (var state in __instance.playerStates)
                    {
                        if (player.PlayerId != state.TargetPlayerId) continue;
                        if (CustomGameOptions.SeerInfo == SeerInfo.Roles) {
                            var role = Role.GetRole(player);
                            state.NameText.color = Color.white;
                            if (role.Name == "Crewmate" || role.Name == "Impostor" || role.Name == "Shifter" || role.Name == "Cannibal") {
                                state.NameText.text = NameText(player, " <color=#FFFFFFFF>Crw</color>/<color=#FF0000FF>Imp</color>/<color=#999999FF>Shi</color>/<color=#AC8A00FF>Can</color>", true);
                            } else if (role.Name == "Mayor" || role.Name == "Jester" || role.Name == "Swapper") {
                                state.NameText.text = NameText(player, " <color=#704FA8FF>Myr</color>/<color=#FFBFCCFF>Jes</color>/<color=#66E666FF>Swp</color>", true);
                            } else if (role.Name == "Sheriff" || role.Name == "The Glitch" || role.Name == "Morphling" || role.Name == "Framer") {
                                state.NameText.text = NameText(player, " <color=#FFFF00FF>Shr</color>/<color=#00FF00FF>Gli</color>/<color=#FF0000FF>Mor</color>/<color=#FF0000FF>Fra</color>", true);
                            } else if (role.Name == "Engineer" || role.Name == "Miner" || role.Name == "Arsonist") {
                                state.NameText.text = NameText(player, " <color=#FFA60AFF>Eng</color>/<color=#FF0000FF>Min</color>/<color=#FF4D00FF>Ars</color>", true);
                            } else if (role.Name == "Altruist" || role.Name == "Time Lord" || role.Name == "Undertaker") {
                                state.NameText.text = NameText(player, " <color=#660000FF>Alt</color>/<color=#0000FFFF>Tim</color>/<color=#FF0000FF>Utk</color>", true);
                            } else if (role.Name == "Investigator" || role.Name == "Tracker" || role.Name == "Executioner") {
                                state.NameText.text = NameText(player, " <color=#00B3B3FF>Inv</color>/<color=#00B3B3FF>Tra</color>/<color=#8C4005FF>Exe</color>", true);
                            } else if (role.Name == "Medic" || role.Name == "Lover" || role.Name == "Loving Impostor" || role.Name == "Janitor") {
                                state.NameText.text = NameText(player, " <color=#006600FF>Med</color>/<color=#FF0000FF>Jan</color>/<color=#FF66CCFF>Lov</color>/<color=#FF0000FF>LovImp</color>", true);
                            } else if (role.Name == "Snitch" || role.Name == "Spy" || role.Name == "Underdog" || role.Name == "Poisoner") {
                                state.NameText.text = NameText(player, " <color=#D4AF37FF>Sni</color>/<color=#CCA3CCFF>Spy</color>/<color=#FF0000FF>Poi</color>", true);
                            } else if (role.Name == "Vigilante" || role.Name == "Assassin" || role.Name == "Swooper") {
                                state.NameText.text = NameText(player, " <color=#FFFF00FF>Vig</color>/<color=#FF0000FF>Ass</color>/<color=#FF0000FF>Swo</color>", true);
                            } else if (role.Name == "Camouflager" || role.Name == "Chameleon" || role.Name == "Grenadier") {
                                state.NameText.text = NameText(player, " <color=#FF0000FF>Cam</color>/<color=#00FF00FF>Cha</color>/<color=#FF0000FF>Gre</color>", true);
                            } else {
                                state.NameText.text = NameText(player, " <color=#FFFFFFFF>Crw</color>/<color=#FF0000FF>Imp</color>", true);
                            }
                        } else {
                            var roleType = Utils.GetRole(player);
                            switch (roleType)
                            {
                                case RoleEnum.Crewmate:
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    state.NameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.green : Color.white;
                                    state.NameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "", true);
                                } else {
                                    state.NameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.red : Color.white;
                                    state.NameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Impostor)" : "", true);
                                }
                                    break;
                                case RoleEnum.Impostor:
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    state.NameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.red : Palette.ImpostorRed;
                                    state.NameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Imp)" : "", true);
                                } else {
                                    state.NameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.green : Color.white;
                                    state.NameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "", true);
                                }
                                    break;
                                default:
                                    var role = Role.GetRole(player);
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    state.NameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? role.FactionColor : role.Color;
                                    state.NameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? $" ({role.Name})" : "", true);
                                } else {
                                    state.NameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.green : Color.white;
                                    state.NameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "", true);
                                }
                                    break;
                            }
                        }
                    }
                }
            }
        }

        [HarmonyPriority(Priority.Last)]
        private static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            foreach (var role in Role.GetRoles(RoleEnum.Seer))
            {
                var seerRole = (Seer) role;
                if (!seerRole.Investigated.Contains(PlayerControl.LocalPlayer.PlayerId)) continue;
                if (!seerRole.CheckSeeReveal(PlayerControl.LocalPlayer)) continue;

                seerRole.Player.nameText.color = seerRole.Color;
                seerRole.Player.nameText.text = NameText(seerRole.Player, " (Seer)");
            }

            if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance);
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Seer)) return;
            var seer = Role.GetRole<Seer>(PlayerControl.LocalPlayer);
            if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance, seer);

            if (!PlayerControl.LocalPlayer.Data.IsDead) {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!seer.Investigated.Contains(player.PlayerId)) continue;
                        player.nameText.transform.localPosition = new Vector3(0f, 2f, -0.5f);
                        if (CustomGameOptions.SeerInfo == SeerInfo.Roles) {
                            var role = Role.GetRole(player);
                            player.nameText.color = Color.white;
                            if (role.Name == "Crewmate" || role.Name == "Impostor" || role.Name == "Shifter" || role.Name == "Cannibal") {
                                player.nameText.text = NameText(player, " <color=#FFFFFFFF>Crw</color>/<color=#FF0000FF>Imp</color>/<color=#999999FF>Shi</color>/<color=#AC8A00FF>Can</color>", true);
                            } else if (role.Name == "Mayor" || role.Name == "Jester" || role.Name == "Swapper") {
                                player.nameText.text = NameText(player, " <color=#704FA8FF>Myr</color>/<color=#FFBFCCFF>Jes</color>/<color=#66E666FF>Swp</color>", true);
                            } else if (role.Name == "Sheriff" || role.Name == "The Glitch" || role.Name == "Morphling" || role.Name == "Framer") {
                                player.nameText.text = NameText(player, " <color=#FFFF00FF>Shr</color>/<color=#00FF00FF>Gli</color>/<color=#FF0000FF>Mor</color>/<color=#FF0000FF>Fra</color>", true);
                            } else if (role.Name == "Engineer" || role.Name == "Miner" || role.Name == "Arsonist") {
                                player.nameText.text = NameText(player, " <color=#FFA60AFF>Eng</color>/<color=#FF0000FF>Min</color>/<color=#FF4D00FF>Ars</color>", true);
                            } else if (role.Name == "Altruist" || role.Name == "Time Lord" || role.Name == "Undertaker") {
                                player.nameText.text = NameText(player, " <color=#660000FF>Alt</color>/<color=#0000FFFF>Tim</color>/<color=#FF0000FF>Utk</color>", true);
                            } else if (role.Name == "Investigator" || role.Name == "Tracker" || role.Name == "Executioner") {
                                player.nameText.text = NameText(player, " <color=#00B3B3FF>Inv</color>/<color=#00B3B3FF>Tra</color>/<color=#8C4005FF>Exe</color>", true);
                            } else if (role.Name == "Medic" || role.Name == "Lover" || role.Name == "Loving Impostor" || role.Name == "Janitor") {
                                player.nameText.text = NameText(player, " <color=#006600FF>Med</color>/<color=#FF0000FF>Jan</color>/<color=#FF66CCFF>Lov</color>/<color=#FF0000FF>LovImp</color>", true);
                            } else if (role.Name == "Snitch" || role.Name == "Spy" || role.Name == "Underdog" || role.Name == "Poisoner") {
                                player.nameText.text = NameText(player, " <color=#D4AF37FF>Sni</color>/<color=#CCA3CCFF>Spy</color>/<color=#FF0000FF>Poi</color>", true);
                            } else if (role.Name == "Vigilante" || role.Name == "Assassin" || role.Name == "Swooper") {
                                player.nameText.text = NameText(player, " <color=#FFFF00FF>Vig</color>/<color=#FF0000FF>Ass</color>/<color=#FF0000FF>Swo</color>", true);
                            } else if (role.Name == "Camouflager" || role.Name == "Chameleon" || role.Name == "Grenadier") {
                                player.nameText.text = NameText(player, " <color=#FF0000FF>Cam</color>/<color=#00FF00FF>Cha</color>/<color=#FF0000FF>Gre</color>", true);
                            } else {
                                player.nameText.text = NameText(player, " <color=#FFFFFFFF>Crw</color>/<color=#FF0000FF>Imp</color>", true);
                            }
                        } else {
                        var roleType = Utils.GetRole(player);
                        switch (roleType)
                        {
                            case RoleEnum.Crewmate:
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    player.nameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.green : Color.white;
                                    player.nameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "");
                                } else {
                                    player.nameText.color =
                                        CustomGameOptions.SeerInfo == SeerInfo.Faction ? Color.red : Color.white;
                                    player.nameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Impostor)" : "");
                                }
                                break;
                            case RoleEnum.Impostor:
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    player.nameText.color = CustomGameOptions.SeerInfo == SeerInfo.Faction
                                        ? Color.red
                                        : Palette.ImpostorRed;
                                    player.nameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Imp)" : "");
                                } else {
                                    player.nameText.color = CustomGameOptions.SeerInfo == SeerInfo.Faction
                                        ? Color.green
                                        : Color.white;
                                    player.nameText.text = NameText(player,
                                        CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "");
                                }
                                break;
                            default:
                                var role = Role.GetRole(player);
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                player.nameText.color = CustomGameOptions.SeerInfo == SeerInfo.Faction
                                    ? role.FactionColor
                                    : role.Color;
                                player.nameText.text = NameText(player,
                                    CustomGameOptions.SeerInfo == SeerInfo.Role ? $" ({role.Name})" : "");
                                } else {
                                player.nameText.color = CustomGameOptions.SeerInfo == SeerInfo.Faction
                                    ? Color.green
                                    : Color.white;
                                player.nameText.text = NameText(player,
                                    CustomGameOptions.SeerInfo == SeerInfo.Role ? " (Crew)" : "");
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}