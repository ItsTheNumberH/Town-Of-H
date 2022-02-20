using Hazel;
using System;
using System.Linq;
using TownOfUs.CrewmateRoles.MedicMod;
using UnityEngine;
using TownOfUs.Extensions;

namespace TownOfUs.Roles
{
    public class Juggernaut : Role
    {
        public Juggernaut(PlayerControl owner) : base(owner)
        {
            Name = "Juggernaut";
            Color = Patches.Colors.Juggernaut;
            LastKill = DateTime.UtcNow;
            KillTarget = null;
            RoleType = RoleEnum.Juggernaut;
            AddToRoleHistory(RoleType);
            ImpostorText = () => "With each kill your kill cooldown decreases";
            TaskText = () => "With each kill your cooldown decreases";
            Faction = Faction.Neutral;
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastKill { get; set; }
        public PlayerControl KillTarget { get; set; }
        public bool JuggernautWins { get; set; }
        public int JuggKills { get; set; } = 0;

        internal override bool EABBNOODFGL(ShipStatus __instance)
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return true;

            if (PlayerControl.AllPlayerControls.ToArray().Count(x => !x.Data.IsDead && !x.Data.Disconnected) == 1)
            {
                var writer = AmongUsClient.Instance.StartRpcImmediately(
                    PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.JuggernautWin,
                    SendOption.Reliable,
                    -1
                );
                writer.Write(Player.PlayerId);
                Wins();
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                Utils.EndGame();
                return false;
            }

            return false;
        }

        public void Wins()
        {
            //System.Console.WriteLine("Reached Here - Glitch Edition");
            JuggernautWins = true;
        }

        public void Loses()
        {
            LostByRPC = true;
        }

        public void Update(HudManager __instance)
        {
            if (!Player.Data.IsDead)
            {
                Utils.SetClosestPlayer(ref ClosestPlayer);
            }

            Player.nameText.color = Color;

            if (MeetingHud.Instance != null)
                foreach (var player in MeetingHud.Instance.playerStates)
                    if (player.NameText != null && Player.PlayerId == player.TargetPlayerId)
                        player.NameText.color = Color;

            if (HudManager.Instance != null && HudManager.Instance.Chat != null)
                foreach (var bubble in HudManager.Instance.Chat.chatBubPool.activeChildren)
                    if (bubble.Cast<ChatBubble>().NameText != null &&
                        Player.Data.PlayerName == bubble.Cast<ChatBubble>().NameText.text)
                        bubble.Cast<ChatBubble>().NameText.color = Color;

            FixedUpdate(__instance);
        }

        public void FixedUpdate(HudManager __instance)
        {
            KillButtonHandler.KillButtonUpdate(this, __instance);

            if (__instance.KillButton != null && Player.Data.IsDead)
                __instance.KillButton.SetTarget(null);
        }

        public bool UseAbility(KillButton __instance)
        {
            KillButtonHandler.KillButtonPress(this, __instance);

            return false;
        }

        public static class KillButtonHandler
        {
            public static void KillButtonUpdate(Juggernaut __gInstance, HudManager __instance)
            {
                if (!__gInstance.Player.Data.IsImpostor() && Input.GetKeyDown(KeyCode.Q))
                    __instance.KillButton.DoClick();

                __instance.KillButton.gameObject.SetActive(__instance.UseButton.isActiveAndEnabled &&
                                                           !__gInstance.Player.Data.IsDead);
                __instance.KillButton.SetCoolDown(
                    CustomGameOptions.GlitchKillCooldown + 5.0f - 5.0f * __gInstance.JuggKills -
                    (float)(DateTime.UtcNow - __gInstance.LastKill).TotalSeconds,
                    CustomGameOptions.GlitchKillCooldown + 5.0f);

                __instance.KillButton.SetTarget(null);
                __gInstance.KillTarget = null;

                if (__instance.KillButton.isActiveAndEnabled)
                {
                    __instance.KillButton.SetTarget(__gInstance.ClosestPlayer);
                    __gInstance.KillTarget = __gInstance.ClosestPlayer;
                }

                if (__gInstance.KillTarget != null)
                    __gInstance.KillTarget.myRend.material.SetColor("_OutlineColor", __gInstance.Color);
            }

            public static void KillButtonPress(Juggernaut __gInstance, KillButton __instance)
            {
                if (__gInstance.KillTarget != null)
                {
                    if (__gInstance.KillTarget.IsOnAlert())
                    {
                        if (__gInstance.KillTarget.IsShielded())
                        {
                            var medic = __gInstance.KillTarget.GetMedic().Player.PlayerId;
                            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                                (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                            writer.Write(medic);
                            writer.Write(__gInstance.KillTarget.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);

                            if (CustomGameOptions.ShieldBreaks) __gInstance.LastKill = DateTime.UtcNow;

                            StopKill.BreakShield(medic, __gInstance.KillTarget.PlayerId,
                                CustomGameOptions.ShieldBreaks);
                            Utils.RpcMurderPlayer(__gInstance.KillTarget, __gInstance.Player);
                        }
                        else if (__gInstance.Player.IsShielded())
                        {
                            var medic = __gInstance.Player.GetMedic().Player.PlayerId;
                            var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                                (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                            writer.Write(medic);
                            writer.Write(__gInstance.Player.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);

                            if (CustomGameOptions.ShieldBreaks) __gInstance.LastKill = DateTime.UtcNow;

                            StopKill.BreakShield(medic, __gInstance.Player.PlayerId,
                                CustomGameOptions.ShieldBreaks);
                            if (CustomGameOptions.KilledOnAlert)
                            {
                                Utils.RpcMurderPlayer(__gInstance.Player, __gInstance.KillTarget);
                            }
                        }
                        else
                        {
                            Utils.RpcMurderPlayer(__gInstance.KillTarget, __gInstance.Player);
                            if (CustomGameOptions.KilledOnAlert)
                            {
                                Utils.RpcMurderPlayer(__gInstance.Player, __gInstance.KillTarget);
                            }
                        }

                        return;
                    }
                    else if (__gInstance.KillTarget.IsShielded())
                    {
                        var medic = __gInstance.KillTarget.GetMedic().Player.PlayerId;
                        var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                        writer.Write(medic);
                        writer.Write(__gInstance.KillTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);

                        if (CustomGameOptions.ShieldBreaks) __gInstance.LastKill = DateTime.UtcNow;

                        StopKill.BreakShield(medic, __gInstance.KillTarget.PlayerId,
                            CustomGameOptions.ShieldBreaks);

                        return;
                    }

                    __gInstance.LastKill = DateTime.UtcNow;
                    __gInstance.JuggKills = __gInstance.JuggKills + 1;
                    __gInstance.Player.SetKillTimer(CustomGameOptions.GlitchKillCooldown + 5.0f - 5.0f * __gInstance.JuggKills);
                    Utils.RpcMurderPlayer(__gInstance.Player, __gInstance.KillTarget);
                }
            }
        }

        protected override void IntroPrefix(IntroCutscene._CoBegin_d__18 __instance)
        {
            var juggTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            juggTeam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = juggTeam;
        }
    }
}