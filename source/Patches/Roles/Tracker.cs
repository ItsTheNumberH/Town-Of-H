using System;
using System.Collections.Generic;
using TownOfUs.CustomHats;
using TownOfUs.ImpostorRoles.CamouflageMod;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Tracker : Role
    {
        public List<ArrowBehaviour> TrackerArrows = new List<ArrowBehaviour>();
        public List<PlayerControl> TrackerTargets = new List<PlayerControl>();
        public PlayerControl ClosestPlayer;
        public bool UsedTrack { get; set; } = false;
        public List<byte> Tracked = new List<byte>();
        public DateTime LastTracked { get; set; }

        public KillButtonManager _trackButton;

        public Tracker(PlayerControl player) : base(player)
        {
            Name = "Tracker";
            ImpostorText = () => "Track a players movement";
            TaskText = () => "Track a players movement";
            Color = new Color(0f, 0.7f, 0.7f, 1f);
            RoleType = RoleEnum.Tracker;
        }
        public float TrackerTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastTracked;
            var num = CustomGameOptions.TrackerCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }


        public KillButtonManager TrackButton
        {
            get => _trackButton;
            set
            {
                _trackButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }
    }
}