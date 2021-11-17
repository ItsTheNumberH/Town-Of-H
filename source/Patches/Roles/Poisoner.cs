using System;
using TownOfUs.Extensions;
using TownOfUs.Roles.Modifiers;
using UnityEngine;
using Hazel;

namespace TownOfUs.Roles
{
    public class Poisoner : Role

    {
        public KillButtonManager _poisonButton;
        public PlayerControl ClosestPlayer;
        public DateTime LastPoisoned;
        public PlayerControl PoisonedPlayer;
        public float TimeRemaining;
        public bool poisonAlerted = false;

        public Poisoner(PlayerControl player) : base(player)
        {
            Name = "Poisoner";
            ImpostorText = () => "Poison the crewmates";
            TaskText = () => "Poison a crewmate to kill them in a few seconds";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Poisoner;
            Faction = Faction.Impostors;
            PoisonedPlayer = null;
        }
        public KillButtonManager PoisonButton
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
            TimeRemaining -= Time.deltaTime;
            if (!poisonAlerted && TimeRemaining <= CustomGameOptions.PoisonAlertDelay) {
                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.PoisonAlert,
                    SendOption.Reliable, -1);
                writer.Write(PlayerControl.LocalPlayer.PlayerId);
                writer.Write(PoisonedPlayer.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                poisonAlerted = true;
            } else if (TimeRemaining <= 0) {
                PoisonKill();
                poisonAlerted = false;
            }
        }
        public void PoisonKill()
        {
            Utils.RpcMurderPlayer(PlayerControl.LocalPlayer, PoisonedPlayer);
            PoisonedPlayer = null;
            LastPoisoned = DateTime.UtcNow;
            SoundManager.Instance.PlaySound(PlayerControl.LocalPlayer.KillSfx, false, 0.5f);
        }
        public float PoisonTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastPoisoned;
            var num = CustomGameOptions.PoisonerCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}