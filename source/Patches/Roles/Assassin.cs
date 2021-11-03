﻿using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Assassin : Role
    {
        public Dictionary<byte, (GameObject, GameObject, TMP_Text)> Buttons = new Dictionary<byte, (GameObject, GameObject, TMP_Text)>();


        public Dictionary<string, Color> ColorMapping = new Dictionary<string, Color>
        {
            { "Altruist", new Color(0.4f, 0f, 0f, 1f) },
            { "Engineer", new Color(1f, 0.65f, 0.04f, 1f) },
            { "Investigator", new Color(0f, 0.7f, 0.7f, 1f) },
            { "Lover", new Color(1f, 0.4f, 0.8f, 1f) },
            { "Mayor", new Color(0.44f, 0.31f, 0.66f, 1f) },
            { "Medic", new Color(0f, 0.4f, 0f, 1f) },
            { "Seer", new Color(1f, 0.8f, 0.5f, 1f) },
            { "Sheriff", Color.yellow },
            { "Snitch", new Color(0.83f, 0.69f, 0.22f, 1f) },
            { "Spy", new Color(0.8f, 0.64f, 0.8f, 1f) },
            { "Swapper", new Color(0.4f, 0.9f, 0.4f, 1f) },
            { "Time Lord", new Color(0f, 0f, 1f, 1f) },
            { "Tracker", new Color(0f, 0.7f, 0.7f, 1f) },
            { "Vigilante", Color.yellow }
        };

        public Dictionary<byte, string> Guesses = new Dictionary<byte, string>();


        public Assassin(PlayerControl player) : base(player)
        {
            Name = "Assassin";
            ImpostorText = () => "Kill during meetings if you can guess their roles";
            TaskText = () => "Guess the roles of the people and kill them mid-meeting";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Assassin;
            Faction = Faction.Impostors;

            RemainingKills = CustomGameOptions.AssassinKills;

            if (CustomGameOptions.AssassinCrewmateGuess) ColorMapping.Add("Crewmate", Color.white);
            if (CustomGameOptions.AssassinGuessChameleon) ColorMapping.Add("Chameleon", Color.green);
            if (CustomGameOptions.AssassinGuessNeutrals)
            {
                ColorMapping.Add("Jester", new Color(1f, 0.75f, 0.8f, 1f));
                ColorMapping.Add("Shifter", new Color(0.6f, 0.6f, 0.6f, 1f));
                ColorMapping.Add("Executioner", new Color(0.55f, 0.25f, 0.02f, 1f));
                ColorMapping.Add("The Glitch", Color.green);
                ColorMapping.Add("Arsonist", new Color(1f, 0.3f, 0f));
            }

        }

        public bool GuessedThisMeeting { get; set; } = false;

        public int RemainingKills { get; set; }

        public List<string> PossibleGuesses => ColorMapping.Keys.ToList();
    }
}
