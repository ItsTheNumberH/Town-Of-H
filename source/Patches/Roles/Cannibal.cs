using System;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Cannibal : Role
    {
        public KillButtonManager _eatButton;
        public bool CannibalWins;
        public int bodiesEaten = 0;
        public DateTime lastEaten;
        public List<ArrowBehaviour> bodyArrows = new List<ArrowBehaviour>();
        public List<Component> bodyTargets = new List<Component>();

        public Cannibal(PlayerControl player) : base(player)
        {
            Name = "Cannibal";
            ImpostorText = () => "Eat bodies";
            TaskText = () => "Eat enough bodies.";
            Color = Palette.Brown;
            RoleType = RoleEnum.Cannibal;
            Faction = Faction.Neutral;
        }

        public DeadBody CurrentTarget { get; set; }

        public KillButtonManager EatButton
        {
            get => _eatButton;
            set
            {
                _eatButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public void Wins()
        {
            //System.Console.WriteLine("Reached Here - Glitch Edition");
            CannibalWins = true;
        }

        public void Loses()
        {
            Player.Data.IsImpostor = true;
        }
    }
}