using HarmonyLib;
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
                if (meeting) return player.name + str;

                return "";
            }

            return player.name + str;
        }

        private static void UpdateMeeting(MeetingHud __instance, Seer seer)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (!seer.Investigated.Contains(player.PlayerId)) continue;
                foreach (var state in __instance.playerStates)
                {
                    if (player.PlayerId != state.TargetPlayerId) continue;
                    var roleType = Utils.GetRole(player);
                    switch (roleType)
                    {
                        default:
                            if ((player.Is(Faction.Crewmates) && !(player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante))) ||
                            ((player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante)) && !CustomGameOptions.CrewKillingRed) ||
                            ((player.Is(RoleEnum.Amnesiac) || player.Is(RoleEnum.Survivor) || player.Is(RoleEnum.GuardianAngel)) && !CustomGameOptions.NeutBenignRed) ||
                            ((player.Is(RoleEnum.Executioner) || player.Is(RoleEnum.Jester) || player.Is(RoleEnum.Phantom)) && !CustomGameOptions.NeutEvilRed) ||
                            ((player.Is(RoleEnum.Arsonist) || player.Is(RoleEnum.Glitch) || player.Is(RoleEnum.Juggernaut)) && !CustomGameOptions.NeutKillingRed))
                            {
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    state.NameText.color = Color.green;
                                } else {
                                    state.NameText.color = Color.red;
                                }
                            }
                            else
                            {
                                if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                    state.NameText.color = Color.red;
                                } else {
                                    state.NameText.color = Color.green;
                                }
                            }
                            break;
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
            if (PlayerControl.LocalPlayer.Data.IsDead) return;

            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Seer)) return;
            var seer = Role.GetRole<Seer>(PlayerControl.LocalPlayer);
            if (MeetingHud.Instance != null) UpdateMeeting(MeetingHud.Instance, seer);


            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (!seer.Investigated.Contains(player.PlayerId)) continue;
                var roleType = Utils.GetRole(player);
                player.nameText.transform.localPosition = new Vector3(0f, 2f, -0.5f);
                switch (roleType)
                {
                    default:
                        if ((player.Is(Faction.Crewmates) && !(player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante))) ||
                            ((player.Is(RoleEnum.Sheriff) || player.Is(RoleEnum.Veteran) || player.Is(RoleEnum.Vigilante)) && !CustomGameOptions.CrewKillingRed) ||
                            ((player.Is(RoleEnum.Amnesiac) || player.Is(RoleEnum.Survivor) || player.Is(RoleEnum.GuardianAngel)) && !CustomGameOptions.NeutBenignRed) ||
                            ((player.Is(RoleEnum.Executioner) || player.Is(RoleEnum.Jester) || player.Is(RoleEnum.Phantom)) && !CustomGameOptions.NeutEvilRed) ||
                            ((player.Is(RoleEnum.Arsonist) || player.Is(RoleEnum.Glitch) || player.Is(RoleEnum.Juggernaut)) && !CustomGameOptions.NeutKillingRed))
                        {
                            if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                player.nameText.color = Color.green;
                            } else {
                                player.nameText.color = Color.red;
                            }
                        }
                        else
                        {
                            if (CustomGameOptions.SeerAccuracy == 100 || seer.randomSeerAccuracy <= CustomGameOptions.SeerAccuracy) {
                                player.nameText.color = Color.red;
                            } else {
                                player.nameText.color = Color.green;  
                            }
                        }
                        break;
                }
            }
        }
    }
}