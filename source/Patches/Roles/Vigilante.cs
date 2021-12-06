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
            
            if (CustomGameOptions.AssassinOn > 0) ColorMapping.Add("Assassin", Palette.ImpostorRed);
            if (CustomGameOptions.CamouflagerOn > 0) ColorMapping.Add("Camouflager", Palette.ImpostorRed);
            if (CustomGameOptions.FramerOn > 0) ColorMapping.Add("Framer", Palette.ImpostorRed);
            if (CustomGameOptions.FreezerOn > 0) ColorMapping.Add("Freezer", Palette.ImpostorRed);
            if (CustomGameOptions.GrenadierOn > 0) ColorMapping.Add("Grenadier", Palette.ImpostorRed);
            if (CustomGameOptions.VigilanteImpostorGuess) ColorMapping.Add("Impostor", Palette.ImpostorRed);
            if (CustomGameOptions.JanitorOn > 0) ColorMapping.Add("Janitor", Palette.ImpostorRed);
            if (CustomGameOptions.LoversOn > 0) ColorMapping.Add("Loving Impostor", Palette.ImpostorRed);
            if (CustomGameOptions.MinerOn > 0) ColorMapping.Add("Miner", Palette.ImpostorRed);
            if (CustomGameOptions.MorphlingOn > 0) ColorMapping.Add("Morphling", Palette.ImpostorRed);
            if (CustomGameOptions.PoisonerOn > 0) ColorMapping.Add("Poisoner", Palette.ImpostorRed);
            if (CustomGameOptions.PuppeteerOn > 0) ColorMapping.Add("Puppeteer", Palette.ImpostorRed);
            if (CustomGameOptions.SwooperOn > 0) ColorMapping.Add("Swooper", Palette.ImpostorRed);
            if (CustomGameOptions.UnderdogOn > 0) ColorMapping.Add("Underdog", Palette.ImpostorRed);
            if (CustomGameOptions.UndertakerOn > 0) ColorMapping.Add("Undertaker", Palette.ImpostorRed);

            if (CustomGameOptions.VigilanteGuessNeutrals)
            {
                if (CustomGameOptions.ArsonistOn > 0) ColorMapping.Add("Arsonist", new Color(1f, 0.3f, 0f));
                if (CustomGameOptions.CannibalOn > 0) ColorMapping.Add("Cannibal", Palette.Brown);
                if (CustomGameOptions.ExecutionerOn > 0) ColorMapping.Add("Executioner", new Color(0.55f, 0.25f, 0.02f, 1f));
                if (CustomGameOptions.GlitchOn > 0) ColorMapping.Add("The Glitch", Color.green);
                if (CustomGameOptions.JesterOn > 0) ColorMapping.Add("Jester", new Color(1f, 0.75f, 0.8f, 1f));
                if (CustomGameOptions.ShifterOn > 0) ColorMapping.Add("Shifter", new Color(0.6f, 0.6f, 0.6f, 1f));
            }
        }

        public bool GuessedThisMeeting { get; set; } = false;

        public int RemainingKills { get; set; }

        public List<string> PossibleGuesses => ColorMapping.Keys.ToList();
    }
}
