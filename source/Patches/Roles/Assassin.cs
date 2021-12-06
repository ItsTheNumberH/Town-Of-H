using System.Collections.Generic;
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


            if (CustomGameOptions.AltruistOn > 0) ColorMapping.Add("Altruist", new Color(0.4f, 0f, 0f, 1f));
            if (CustomGameOptions.ChameleonOn > 0 && CustomGameOptions.AssassinGuessChameleon) ColorMapping.Add("Chameleon", Color.green);
            if (CustomGameOptions.AssassinCrewmateGuess) ColorMapping.Add("Crewmate", Color.white);
            if (CustomGameOptions.EngineerOn > 0) ColorMapping.Add("Engineer", new Color(1f, 0.65f, 0.04f, 1f));
            if (CustomGameOptions.InvestigatorOn > 0) ColorMapping.Add("Investigator", new Color(0f, 0.7f, 0.7f, 1f));
            if (CustomGameOptions.LoversOn > 0) ColorMapping.Add("Lover", new Color(1f, 0.4f, 0.8f, 1f));
            if (CustomGameOptions.MayorOn > 0) ColorMapping.Add("Mayor", new Color(0.44f, 0.31f, 0.66f, 1f));
            if (CustomGameOptions.MedicOn > 0) ColorMapping.Add("Medic", new Color(0f, 0.4f, 0f, 1f));
            if (CustomGameOptions.SeerOn > 0) ColorMapping.Add("Seer", new Color(1f, 0.8f, 0.5f, 1f));
            if (CustomGameOptions.SheriffOn > 0) ColorMapping.Add("Sheriff", Color.yellow);
            if (CustomGameOptions.SnitchOn > 0) ColorMapping.Add("Snitch", new Color(0.83f, 0.69f, 0.22f, 1f));
            if (CustomGameOptions.SpyOn > 0) ColorMapping.Add("Spy", new Color(0.8f, 0.64f, 0.8f, 1f));
            if (CustomGameOptions.SwapperOn > 0) ColorMapping.Add("Swapper", new Color(0.4f, 0.9f, 0.4f, 1f));
            if (CustomGameOptions.TimeLordOn > 0) ColorMapping.Add("Time Lord", new Color(0f, 0f, 1f, 1f));
            if (CustomGameOptions.TrackerOn > 0) ColorMapping.Add("Tracker", new Color(0f, 0.7f, 0.7f, 1f));
            if (CustomGameOptions.VigilanteOn > 0) ColorMapping.Add("Vigilante", Color.yellow);

            if (CustomGameOptions.AssassinGuessNeutrals)
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
