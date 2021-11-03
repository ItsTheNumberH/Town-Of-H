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
            { "Assassin", Palette.ImpostorRed },
            { "Camouflager", Palette.ImpostorRed },
            { "Grenadier", Palette.ImpostorRed },
            { "Janitor", Palette.ImpostorRed },
            { "Miner", Palette.ImpostorRed },
            { "Morphling", Palette.ImpostorRed },
            { "Swooper", Palette.ImpostorRed },
            { "Underdog", Palette.ImpostorRed },
            { "Undertaker", Palette.ImpostorRed },
        };

        public Dictionary<byte, string> Guesses = new Dictionary<byte, string>();


        public Vigilante(PlayerControl player) : base(player)
        {
            Name = "Vigilante";
            ImpostorText = () => "Kill bad people during meetings if you can guess their roles";
            TaskText = () => "Guess the roles of bad people and kill them mid-meeting";
            Color = Color.yellow;
            RoleType = RoleEnum.Vigilante;

            RemainingKills = CustomGameOptions.VigilanteKills;

            if (CustomGameOptions.VigilanteGuessNeutrals)
            {
                ColorMapping.Add("Jester", new Color(1f, 0.75f, 0.8f, 1f));
                ColorMapping.Add("Shifter", new Color(0.6f, 0.6f, 0.6f, 1f));
                ColorMapping.Add("Executioner", new Color(0.55f, 0.25f, 0.02f, 1f));
                ColorMapping.Add("The Glitch", Color.green);
                ColorMapping.Add("Arsonist", new Color(1f, 0.3f, 0f));
            }

            if (CustomGameOptions.VigilanteImpostorGuess) ColorMapping.Add("Impostor", Palette.ImpostorRed);
        }

        public bool GuessedThisMeeting { get; set; } = false;

        public int RemainingKills { get; set; }

        public List<string> PossibleGuesses => ColorMapping.Keys.ToList();
    }
}
