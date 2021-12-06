using System;
using TownOfUs.ImpostorRoles.CamouflageMod;
using TownOfUs.ImpostorRoles.FramerMod;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Framer : Role
    {
        private KillButtonManager _frameButton;
        public DateTime LastFramed { get; set; }
        public float TimeBeforeFramed { get; private set; }
        public float FrameTimeRemaining { get; private set; }
        public PlayerControl Target;
        public PlayerControl Framed { get; private set; }

        public Framer(PlayerControl player) : base(player)
        {
            Name = "Framer";
            ImpostorText = () => "Frame the crewmates";            
            TaskText = () =>
                CustomGameOptions.FrameTarget == FrameTarget.Self
                    ? "Frame crewmates to look like you."
                    : "Frame crewmates to look like another player.";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Framer;
            Faction = Faction.Impostors;
        }

        public KillButtonManager FrameButton
        {
            get => _frameButton;
            set
            {
                _frameButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public float FrameTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastFramed;
            var num = CustomGameOptions.FrameCooldown * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public void StartFrame(PlayerControl framed)
        {
            Framed = framed;
            TimeBeforeFramed = CustomGameOptions.TimeToFrame;
        }

        public void FrameTick()
        {
            if (Framed == null)
            {
                return;
            }

            if (TimeBeforeFramed > 0)
            {
                TimeBeforeFramed = Math.Clamp(TimeBeforeFramed - Time.deltaTime, 0, TimeBeforeFramed);
                if (TimeBeforeFramed <= 0f)
                {
                    FrameTimeRemaining = CustomGameOptions.FrameDuration;
                }
            }
            else if (FrameTimeRemaining > 0)
            {
                FrameTimeRemaining -= Time.deltaTime;
                Frame();
            }
            else
            {
                Unframe();
            }
        }

        private void Frame()
        {
            //Don't show frame for Framer
            if (
                PlayerControl.LocalPlayer.Data.IsImpostor
                || PlayerControl.LocalPlayer.Data.IsDead
                || CamouflageUnCamouflage.IsCamoed
                || Framed == null
                || PlayerControl.LocalPlayer.PlayerId == Framed.PlayerId
                )
            {
                return;
            }

            //Get framer or first alive player, frame as this player
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (CustomGameOptions.FrameTarget == FrameTarget.Random && !player.Data.IsDead && Framed != player && !player.Is(RoleEnum.Framer)) {
                    Utils.Frame(Framed, player);
                    break;
                } else if (CustomGameOptions.FrameTarget == FrameTarget.Self && player.Is(RoleEnum.Framer)) {
                    Utils.Frame(Framed, player);
                    break;
                }
            }
        }

        private void Unframe()
        {
            LastFramed = DateTime.UtcNow;
            if (Framed != null)
            {
                Utils.Unframe(Framed);
                Framed = null;
            }
        }
    }
}
