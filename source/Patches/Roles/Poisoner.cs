using System;
using UnityEngine;
using HarmonyLib;
using Hazel;
using Assassin = TownOfUs.Modifiers.AssassinMod;

namespace TownOfUs.Roles
{
    public class Poisoner : Role

    {
        public KillButton _poisonButton;
        public PlayerControl ClosestPlayer;
        public DateTime LastPoisoned;
        public PlayerControl PoisonedPlayer;
        public float TimeRemaining;
        public bool Enabled = false;
        public bool poisonAlerted = false;

        public Poisoner(PlayerControl player) : base(player)
        {
            Name = "Poisoner";
            ImpostorText = () => $"Poison a crewmate to kill them in {CustomGameOptions.PoisonDuration} seconds";
            TaskText = () => "Poison the crewmates";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Poisoner;
            AddToRoleHistory(RoleType);
            Faction = Faction.Impostors;
            PoisonedPlayer = null;
        }
        public KillButton PoisonButton
        {
            get => _poisonButton;
            set
            {
                _poisonButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }
        public bool Poisoned => TimeRemaining > 0f;
        public void Poison()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            if (MeetingHud.Instance)
            {
                TimeRemaining = 0;
            }
            if (CustomGameOptions.PoisonAlertSwitch && !poisonAlerted && TimeRemaining <= CustomGameOptions.PoisonDuration / 2) {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.PoisonAlert,
                    SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write(PoisonedPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                poisonAlerted = true;
            } else if (TimeRemaining <= 0) {
                poisonAlerted = false;
            }
            if (TimeRemaining <= 0)
            {
                PoisonKill();
            }
        }
        public void PoisonKill()
        {
            if (MeetingHud.Instance) {
                Assassin.AssassinKill.MurderPlayer(PoisonedPlayer, false);
            } else {
                Utils.RpcMurderPlayer(Player, PoisonedPlayer);
            }
            PoisonedPlayer = null;
            Enabled = false;
            LastPoisoned = DateTime.UtcNow;
            SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.5f);
        }
        public float PoisonTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastPoisoned;
            var num = CustomGameOptions.PoisonCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}