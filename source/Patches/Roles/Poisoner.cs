using System;
using TownOfUs.Extensions;
using TownOfUs.Roles.Modifiers;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Poisoner : Role

    {
        public KillButtonManager _poisonButton;
        public PlayerControl ClosestPlayer;
        public DateTime LastPoisoned;
        public PlayerControl PoisonedPlayer;
        public float TimeRemaining;

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
            if (TimeRemaining <= 0) {
                PoisonKill();
            }
        }
        public void PoisonKill()
        {
            Utils.RpcMurderPlayer(PoisonedPlayer, PoisonedPlayer);
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