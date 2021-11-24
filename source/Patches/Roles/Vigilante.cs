using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Vigilante : Role
    {
        public Dictionary<byte, (GameObject, GameObject, TMP_Text)> Buttons = new Dictionary<byte, (GameObject, GameObject, TMP_Text)>();

        public Dictionary<string, Color> ColorMapping = new Dictionary<string, Color>
        {
        };

        public Dictionary<byte, string> Guesses = new Dictionary<byte, string>();

        public Vigilante(PlayerControl player) : base(player)
        {
            Name = "Vigilante";
            ImpostorText = () => "Kill bad roles during meetings";
            TaskText = () => "Guess roles to kill them in meetings";
            Color = Color.yellow;
            RoleType = RoleEnum.Vigilante;

            RemainingKills = CustomGameOptions.VigilanteKills;
            
            ColorMapping.Add("Assassin", Palette.ImpostorRed);
            ColorMapping.Add("Camouflager", Palette.ImpostorRed);
            ColorMapping.Add("Framer", Palette.ImpostorRed);
            ColorMapping.Add("Grenadier", Palette.ImpostorRed);
            if (CustomGameOptions.VigilanteImpostorGuess) ColorMapping.Add("Impostor", Palette.ImpostorRed);
            ColorMapping.Add("Janitor", Palette.ImpostorRed);
            ColorMapping.Add("Loving Impostor", Palette.ImpostorRed);
            ColorMapping.Add("Miner", Palette.ImpostorRed);
            ColorMapping.Add("Morphling", Palette.ImpostorRed);
            ColorMapping.Add("Poisoner", Palette.ImpostorRed);
            ColorMapping.Add("Swooper", Palette.ImpostorRed);
            ColorMapping.Add("Underdog", Palette.ImpostorRed);
            ColorMapping.Add("Undertaker", Palette.ImpostorRed);

            if (CustomGameOptions.VigilanteGuessNeutrals)
            {
                ColorMapping.Add("Arsonist", new Color(1f, 0.3f, 0f));
                ColorMapping.Add("Cannibal", Palette.Brown);
                ColorMapping.Add("Executioner", new Color(0.55f, 0.25f, 0.02f, 1f));
                ColorMapping.Add("The Glitch", Color.green);
                ColorMapping.Add("Jester", new Color(1f, 0.75f, 0.8f, 1f));
                ColorMapping.Add("Shifter", new Color(0.6f, 0.6f, 0.6f, 1f));
            }
        }

        public bool GuessedThisMeeting { get; set; } = false;

        public int RemainingKills { get; set; }

        public List<string> PossibleGuesses => ColorMapping.Keys.ToList();
    }
}
