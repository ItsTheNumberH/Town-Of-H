using System;

namespace TownOfUs.CustomOption
{
    public class Generate
    {
        public static CustomHeaderOption CrewmateRoles;
        public static CustomNumberOption MayorOn;
        public static CustomNumberOption LoversOn;
        public static CustomNumberOption SheriffOn;
        public static CustomNumberOption EngineerOn;
        public static CustomNumberOption SwapperOn;
        public static CustomNumberOption InvestigatorOn;
        public static CustomNumberOption TimeLordOn;
        public static CustomNumberOption MedicOn;
        public static CustomNumberOption SeerOn;
        public static CustomNumberOption SpyOn;
        public static CustomNumberOption SnitchOn;
        public static CustomNumberOption TrackerOn;
        public static CustomNumberOption AltruistOn;
        public static CustomNumberOption ButtonBarryOn;


        public static CustomHeaderOption NeutralRoles;
        public static CustomNumberOption JesterOn;
        public static CustomNumberOption ShifterOn;
        public static CustomNumberOption GlitchOn;
        public static CustomNumberOption ExecutionerOn;
        public static CustomNumberOption ArsonistOn;
        public static CustomHeaderOption GhostRoles;
        public static CustomNumberOption PhantomOn;


        public static CustomHeaderOption ImpostorRoles;
        public static CustomNumberOption JanitorOn;
        public static CustomNumberOption MorphlingOn;
        public static CustomNumberOption PoisonerOn;
        public static CustomNumberOption CamouflagerOn;
        public static CustomNumberOption MinerOn;
        public static CustomNumberOption SwooperOn;
        public static CustomNumberOption UndertakerOn;
        public static CustomNumberOption AssassinOn;
        public static CustomNumberOption VigilanteOn;
        public static CustomNumberOption ChameleonOn;
        public static CustomNumberOption UnderdogOn;
        public static CustomNumberOption GrenadierOn;


        /*
        public static CustomNumberOption SecurityGuardOn ;
            */

        public static CustomHeaderOption Modifiers;
        public static CustomNumberOption BaitOn;
        public static CustomNumberOption ChildOn;
        public static CustomNumberOption DiseasedOn;
        public static CustomNumberOption DrunkOn;
        public static CustomNumberOption FlashOn;
        public static CustomNumberOption BigBoiOn;
        public static CustomNumberOption TiebreakerOn;
        public static CustomNumberOption TorchOn;


        public static CustomHeaderOption CustomGameSettings;
        public static CustomToggleOption ColourblindComms;
        public static CustomToggleOption MeetingColourblind;
        public static CustomToggleOption ImpostorSeeRoles;
        public static CustomToggleOption DeadSeeRoles;
        public static CustomNumberOption MaxImpostorRoles;
        public static CustomNumberOption MaxNeutralRoles;
        public static CustomToggleOption RoleUnderName;
        public static CustomNumberOption VanillaGame;
        public static CustomNumberOption InitialCooldowns;
        public static CustomToggleOption ShowKillAnimation;

        public static CustomHeaderOption Mayor;
        public static CustomNumberOption MayorVoteBank;
        public static CustomToggleOption MayorAnonymous;

        public static CustomHeaderOption Lovers;
        public static CustomToggleOption BothLoversDie;

        public static CustomHeaderOption Sheriff;
        public static CustomToggleOption ShowSheriff;
        public static CustomToggleOption SheriffKillOther;
        public static CustomToggleOption SheriffKillsJester;
        public static CustomToggleOption SheriffKillsGlitch;
        public static CustomToggleOption SheriffKillsArsonist;
        public static CustomToggleOption SheriffKillsShifter;
        public static CustomToggleOption SheriffKillsExecutioner;
        public static CustomNumberOption SheriffKillCd;
        public static CustomToggleOption SheriffBodyReport;


        public static CustomHeaderOption Shifter;
        public static CustomNumberOption ShifterCd;
        public static CustomStringOption WhoShifts;
        public static CustomToggleOption ShiftGlitch;
        public static CustomToggleOption ShifterCrewmate;


        public static CustomHeaderOption Engineer;
        public static CustomStringOption EngineerPer;

        public static CustomHeaderOption Investigator;
        public static CustomNumberOption FootprintSize;
        public static CustomNumberOption FootprintInterval;
        public static CustomNumberOption FootprintDuration;
        public static CustomToggleOption AnonymousFootPrint;
        public static CustomToggleOption VentFootprintVisible;
        public static CustomHeaderOption Tracker;
        public static CustomNumberOption TrackerCooldown;
        public static CustomStringOption TrackPer;
        public static CustomNumberOption TrackerInterval;

        public static CustomHeaderOption TimeLord;
        public static CustomToggleOption RewindRevive;
        public static CustomNumberOption RewindDuration;
        public static CustomNumberOption RewindCooldown;
        public static CustomToggleOption TimeLordVitals;

        public static CustomHeaderOption Medic;
        public static CustomStringOption ShowShielded;
        public static CustomToggleOption MedicReportSwitch;
        public static CustomNumberOption MedicReportNameDuration;
        public static CustomNumberOption MedicReportColorDuration;
        public static CustomStringOption WhoGetsNotification;
        public static CustomToggleOption ShieldBreaks;

        public static CustomHeaderOption Seer;
        public static CustomNumberOption SeerCooldown;
        public static CustomStringOption SeerInfo;
        public static CustomNumberOption SeerAccuracy;
        public static CustomStringOption SeeReveal;
        public static CustomToggleOption NeutralRed;
        public static CustomStringOption SeerPer;
        public static CustomHeaderOption TheGlitch;
        public static CustomNumberOption MimicCooldownOption;
        public static CustomNumberOption MimicDurationOption;
        public static CustomNumberOption HackCooldownOption;
        public static CustomNumberOption HackDurationOption;
        public static CustomNumberOption GlitchKillCooldownOption;
        public static CustomNumberOption InitialGlitchKillCooldownOption;
        public static CustomStringOption GlitchHackDistanceOption;


        public static CustomHeaderOption Morphling;
        public static CustomNumberOption MorphlingCooldown;
        public static CustomNumberOption MorphlingDuration;
        public static CustomHeaderOption Poisoner;
        public static CustomNumberOption PoisonerCooldown;
        public static CustomNumberOption PoisonerDuration;
        public static CustomStringOption WhoGetsPoisonAlert;

        public static CustomHeaderOption Camouflager;
        public static CustomNumberOption CamouflagerCooldown;
        public static CustomNumberOption CamouflagerDuration;

        public static CustomHeaderOption Executioner;
        public static CustomStringOption OnTargetDead;

        public static CustomHeaderOption Snitch;
        public static CustomToggleOption SnitchOnLaunch;
        public static CustomToggleOption SnitchSeesNeutrals;

        public static CustomToggleOption ShowShift;
        public static CustomHeaderOption Altruist;
        public static CustomNumberOption ReviveDuration;
        public static CustomToggleOption AltruistTargetBody;

        public static CustomHeaderOption Miner;
        public static CustomNumberOption MineCooldown;

        public static CustomHeaderOption Swooper;
        public static CustomNumberOption SwoopCooldown;
        public static CustomNumberOption SwoopDuration;

        public static CustomHeaderOption Arsonist;
        public static CustomNumberOption DouseCooldown;
        public static CustomToggleOption ArsonistGameEnd;
        public static CustomHeaderOption Phantom;
        public static CustomToggleOption PhantomSpawnInVent;

        public static CustomHeaderOption Undertaker;
        public static CustomNumberOption DragCooldown;
        public static CustomHeaderOption Underdog;
        public static CustomToggleOption UnderdogPlayers;
        public static CustomHeaderOption Grenadier;
        public static CustomNumberOption GrenadeCooldown;
        public static CustomNumberOption GrenadeDuration;
        public static CustomToggleOption GrenadierVent;

        public static CustomHeaderOption Assassin;
        public static CustomNumberOption AssassinKills;
        public static CustomToggleOption AssassinGuessNeutrals;
        public static CustomToggleOption AssassinGuessChameleon;
        public static CustomToggleOption AssassinCrewmateGuess;
        public static CustomToggleOption AssassinMultiKill;
        public static CustomToggleOption AssassinShowKill;
        public static CustomHeaderOption Vigilante;
        public static CustomNumberOption VigilanteKills;
        public static CustomToggleOption VigilanteGuessNeutrals;
        public static CustomToggleOption VigilanteImpostorGuess;
        public static CustomToggleOption VigilanteMultiKill;
        public static CustomToggleOption VigilanteShowKill;
        public static CustomHeaderOption Chameleon;
        public static CustomNumberOption ChameleonCooldown;
        public static CustomNumberOption ChameleonDuration;
        public static Func<object, string> PercentFormat { get; } = value => $"{value:0}%";
        private static Func<object, string> CooldownFormat { get; } = value => $"{value:0.0#}s";
        private static Func<object, string> PlusFormat { get; } = value => $"+{value:0.0#}";
        public static CustomNumberOption FlashSpeed;


        public static void GenerateAll()
        {
            var num = 0;

            Patches.ExportButton = new Export(num++);
            Patches.ImportButton = new Import(num++);

            /* CREWMATE ROLES */
            CrewmateRoles = new CustomHeaderOption(num++, "Crewmate Roles");
            AltruistOn = new CustomNumberOption(true, num++, "<color=#660000FF>Altruist</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            ChameleonOn = new CustomNumberOption(true, num++, "<color=#00FF00FF>Chameleon</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            EngineerOn = new CustomNumberOption(true, num++, "<color=#FFA60AFF>Engineer</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            InvestigatorOn = new CustomNumberOption(true, num++, "<color=#00B3B3FF>Investigator</color>", 0f, 0f, 100f,
                10f, PercentFormat);
            LoversOn = new CustomNumberOption(true, num++, "<color=#FF66CCFF>Lovers</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MayorOn = new CustomNumberOption(true, num++, "<color=#704FA8FF>Mayor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MedicOn = new CustomNumberOption(true, num++, "<color=#006600FF>Medic</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SheriffOn = new CustomNumberOption(true, num++, "<color=#FFFF00FF>Sheriff</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SwapperOn = new CustomNumberOption(true, num++, "<color=#66E666FF>Swapper</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TimeLordOn = new CustomNumberOption(true, num++, "<color=#0000FFFF>Time Lord</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TrackerOn = new CustomNumberOption(true, num++, "<color=#00B3B3FF>Tracker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SeerOn = new CustomNumberOption(true, num++, "<color=#FFCC80FF>Seer</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SnitchOn = new CustomNumberOption(true, num++, "<color=#D4AF37FF>Snitch</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SpyOn = new CustomNumberOption(true, num++, "<color=#CCA3CCFF>Spy</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            VigilanteOn = new CustomNumberOption(true, num++, "<color=#FFFF00FF>Vigilante</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            /*NEUTRAL ROLES */
            NeutralRoles = new CustomHeaderOption(num++, "Neutral Roles");
            ArsonistOn = new CustomNumberOption(true, num++, "<color=#FF4D00FF>Arsonist</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            ExecutionerOn = new CustomNumberOption(true, num++, "<color=#8C4005FF>Executioner</color>", 0f, 0f, 100f,
                10f, PercentFormat);
            GlitchOn = new CustomNumberOption(true, num++, "<color=#00FF00FF>Glitch</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            JesterOn = new CustomNumberOption(true, num++, "<color=#FFBFCCFF>Jester</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            ShifterOn = new CustomNumberOption(true, num++, "<color=#999999FF>Shifter</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            /* GHOST ROLES */
            GhostRoles = new CustomHeaderOption(num++, "Ghost Roles");
            PhantomOn = new CustomNumberOption(true, num++, "<color=#662962>Phantom</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            /* IMPOSTOR ROLES */
            ImpostorRoles = new CustomHeaderOption(num++, "Impostor Roles");
            AssassinOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Assassin</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            CamouflagerOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Camouflager</color>", 0f, 0f, 100f,
                10f, PercentFormat);
            GrenadierOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Grenadier</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            JanitorOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Janitor</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MinerOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Miner</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            MorphlingOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Morphling</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            PoisonerOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Poisoner</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            SwooperOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Swooper</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            UnderdogOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Underdog</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            UndertakerOn = new CustomNumberOption(true, num++, "<color=#FF0000FF>Undertaker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            /* MODIFIERS */
            Modifiers = new CustomHeaderOption(num++, "Modifiers");
            BaitOn =
                new CustomNumberOption(true, num++, "<color=#00B3B3FF>Bait</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
            ButtonBarryOn =
                new CustomNumberOption(true, num++, "<color=#E600FFFF>Button Barry</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
            ChildOn = new CustomNumberOption(true, num++, "<color=#FFFFFFFF>Child</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            DiseasedOn =
                new CustomNumberOption(true, num++, "<color=#808080FF>Diseased</color>", 0f, 0f, 100f, 10f,
                    PercentFormat);
            DrunkOn = new CustomNumberOption(true, num++, "<color=#758000FF>Drunk</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            FlashOn = new CustomNumberOption(true, num++, "<color=#FF8080FF>Flash</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            FlashSpeed =
                new CustomNumberOption(true, num++, "<color=#FF8080FF>Flash speed</color>", 1f, 1f, 3f, 0.1f,
                PlusFormat);
            BigBoiOn = new CustomNumberOption(true, num++, "<color=#FF8080FF>Giant</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TiebreakerOn = new CustomNumberOption(true, num++, "<color=#99E699FF>Tiebreaker</color>", 0f, 0f, 100f, 10f,
                PercentFormat);
            TorchOn = new CustomNumberOption(true, num++, "<color=#FFFF99FF>Torch</color>", 0f, 0f, 100f, 10f,
                PercentFormat);

            /* CUSTOM GAMES SETTINGS */
            CustomGameSettings =
                new CustomHeaderOption(num++, "Custom Game Settings");
            ColourblindComms = new CustomToggleOption(num++, "Camouflaged Comms", false);
            MeetingColourblind = new CustomToggleOption(num++, "Camouflaged Meetings", false);
            ImpostorSeeRoles = new CustomToggleOption(num++, "Impostors can see the roles of their team", false);
            DeadSeeRoles =
                new CustomToggleOption(num++, "Dead can see everyone's roles", false);
            MaxImpostorRoles =
                new CustomNumberOption(num++, "Max Impostor Roles", 1f, 1f, 3f, 1f);
            MaxNeutralRoles =
                new CustomNumberOption(num++, "Max Neutral Roles", 1f, 1f, 5f, 1f);
            RoleUnderName = new CustomToggleOption(num++, "Role Appears Under Name");
            VanillaGame = new CustomNumberOption(num++, "Probability of a completely vanilla game", 0f, 0f, 100f, 5f,
                PercentFormat);
            InitialCooldowns =
                new CustomNumberOption(num++, "Game Start Cooldowns", 10, 10, 30, 2.5f, CooldownFormat);
            ShowKillAnimation = new CustomToggleOption(num++, "Show Kill Animation", true);

            /* ROLE SETTINGS */
            /* ALTRUIST */
            Altruist = new CustomHeaderOption(num++, "<color=#660000FF>Altruist</color>");
            ReviveDuration =
                new CustomNumberOption(num++, "Altruist Revive Duration", 10, 1, 30, 1f, CooldownFormat);
            AltruistTargetBody =
                new CustomToggleOption(num++, "Target's body disappears on beginning of revive", false);

            /* CHAMELEON */
            Chameleon =
                new CustomHeaderOption(num++, "<color=#00FF00FF>Chameleon</color>");
            ChameleonCooldown =
                new CustomNumberOption(num++, "Chameleon Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            ChameleonDuration =
                new CustomNumberOption(num++, "Chameleon Duration", 10, 5, 15, 1f, CooldownFormat);

            /* ENGINEER */
            Engineer =
                new CustomHeaderOption(num++, "<color=#FFA60AFF>Engineer</color>");
            EngineerPer =
                new CustomStringOption(num++, "Engineer Fix Per", new[] {"Round", "Game"});

            /* INVESTIGATOR */
            Investigator =
                new CustomHeaderOption(num++, "<color=#00B3B3FF>Investigator</color>");
            FootprintSize = new CustomNumberOption(num++, "Footprint Size", 4f, 1f, 10f, 1f);
            FootprintInterval =
                new CustomNumberOption(num++, "Footprint Interval", 1f, 0.25f, 5f, 0.25f, CooldownFormat);
            FootprintDuration = new CustomNumberOption(num++, "Footprint Duration", 10f, 1f, 10f, 0.5f, CooldownFormat);
            AnonymousFootPrint = new CustomToggleOption(num++, "Anonymous Footprint", false);
            VentFootprintVisible = new CustomToggleOption(num++, "Footprint Vent Visible", false);

            /* LOVERS */
            Lovers =
                new CustomHeaderOption(num++, "<color=#FF66CCFF>Lovers</color>");
            BothLoversDie = new CustomToggleOption(num++, "Both Lovers Die");

            /* MAYOR */
            Mayor =
                new CustomHeaderOption(num++, "<color=#704FA8FF>Mayor</color>");
            MayorVoteBank =
                new CustomNumberOption(num++, "Initial Mayor Vote Bank", 1, 1, 5, 1);
            MayorAnonymous =
                new CustomToggleOption(num++, "Mayor Votes Show Anonymous", false);

            /* MEDIC */
            Medic =
                new CustomHeaderOption(num++, "<color=#006600FF>Medic</color>");
            ShowShielded =
                new CustomStringOption(num++, "Show Shielded Player",
                    new[] {"Self", "Medic", "Self+Medic", "Everyone"});
            MedicReportSwitch = new CustomToggleOption(num++, "Show Medic Reports");
            MedicReportNameDuration =
                new CustomNumberOption(num++, "Time Where Medic Reports Will Have Name", 0, 0, 60, 2.5f,
                    CooldownFormat);
            MedicReportColorDuration =
                new CustomNumberOption(num++, "Time Where Medic Reports Will Have Color Type", 15, 0, 120, 2.5f,
                    CooldownFormat);
            WhoGetsNotification =
                new CustomStringOption(num++, "Who gets murder attempt indicator",
                    new[] {"Medic", "Shielded", "Everyone", "Nobody"});
            ShieldBreaks = new CustomToggleOption(num++, "Shield breaks on murder attempt", false);

            /* SHERIFF */
            Sheriff =
                new CustomHeaderOption(num++, "<color=#FFFF00FF>Sheriff</color>");
            ShowSheriff = new CustomToggleOption(num++, "Show Sheriff", false);
            SheriffKillOther =
                new CustomToggleOption(num++, "Sheriff Miskill Kills Crewmate", false);
            SheriffKillsJester =
                new CustomToggleOption(num++, "Sheriff Kills Jester", false);
            SheriffKillsGlitch =
                new CustomToggleOption(num++, "Sheriff Kills The Glitch", false);
            SheriffKillsArsonist =
                new CustomToggleOption(num++, "Sheriff Kills Arsonist", false);
            SheriffKillsShifter =
                new CustomToggleOption(num++, "Sheriff Kills Shifter", false);
            SheriffKillsExecutioner =
                new CustomToggleOption(num++, "Sheriff Kills Executioner", false);
            SheriffKillCd =
                new CustomNumberOption(num++, "Sheriff Kill Cooldown", 25f, 10f, 40f, 2.5f, CooldownFormat);
            SheriffBodyReport = new CustomToggleOption(num++, "Sheriff can report who they've killed");

            /* SEER */
            Seer =
                new CustomHeaderOption(num++, "<color=#FFCC80FF>Seer</color>");
            SeerCooldown =
                new CustomNumberOption(num++, "Seer Cooldown", 25f, 10f, 100f, 2.5f, CooldownFormat);
            SeerInfo =
                new CustomStringOption(num++, "Info that Seer sees", new[] {"Role", "Team", "Roles"});
            SeerAccuracy = new CustomNumberOption(num++, "Seer Accuracy", 100f, 0f, 100f, 5f,
                PercentFormat);
            SeeReveal =
                new CustomStringOption(num++, "Who Sees That They Are Revealed",
                    new[] {"Crew", "Imps+Neut", "All", "Nobody"});
            NeutralRed =
                new CustomToggleOption(num++, "Neutrals show up as Impostors", false);
            SeerPer =
                new CustomStringOption(num++, "Seer Reveal Per", new[] {"Round", "Game"});

            /* SNITCH */
            Snitch = new CustomHeaderOption(num++, "<color=#D4AF37FF>Snitch</color>");
            SnitchOnLaunch =
                new CustomToggleOption(num++, "Snitch knows who they are on Game Start", false);
            SnitchSeesNeutrals = new CustomToggleOption(num++, "Snitch sees neutral roles", false);

            /* TIMELORD */
            TimeLord =
                new CustomHeaderOption(num++, "<color=#0000FFFF>Time Lord</color>");
            RewindRevive = new CustomToggleOption(num++, "Revive During Rewind", false);
            RewindDuration = new CustomNumberOption(num++, "Rewind Duration", 3f, 2f, 10f, 0.5f, CooldownFormat);
            RewindCooldown = new CustomNumberOption(num++, "Rewind Cooldown", 25f, 10f, 60f, 2.5f, CooldownFormat);
            TimeLordVitals =
                new CustomToggleOption(num++, "Time Lord can use Vitals", false);

            /* TRACKER */
            Tracker =
                new CustomHeaderOption(num++, "<color=#00B3B3FF>Tracker</color>");
            TrackerCooldown =
                new CustomNumberOption(num++, "Tracker Cooldown", 25f, 10f, 100f, 2.5f, CooldownFormat);
            TrackPer =
                new CustomStringOption(num++, "Track Per", new[] {"Round", "Game"});
            TrackerInterval =
                new CustomNumberOption(num++, "Tracker Interval", 25f, 0f, 50f, 5f, CooldownFormat);

            /* VIGILANTE */
            Vigilante = new CustomHeaderOption(num++, "<color=#FFFF00FF>Vigilante</color>");
            VigilanteKills = new CustomNumberOption(num++, "Number of Vigilante Kills", 1, 1, 5, 1);
            VigilanteImpostorGuess = new CustomToggleOption(num++, "Vigilante can Guess \"Impostor\"", false);
            VigilanteGuessNeutrals = new CustomToggleOption(num++, "Vigilante can Guess Neutral roles", false);
            VigilanteMultiKill = new CustomToggleOption(num++, "Vigilante can kill more than once per meeting");
            VigilanteShowKill = new CustomToggleOption(num++, "Show vigilante kill animation");

            /* ARSONIST */
            Arsonist = new CustomHeaderOption(num++, "<color=#FF4D00FF>Arsonist</color>");
            DouseCooldown =
                new CustomNumberOption(num++, "Douse Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            ArsonistGameEnd = new CustomToggleOption(num++, "Game keeps going so long as Arsonist is alive", false);

            /* EXECUTIONER */
            Executioner =
                new CustomHeaderOption(num++, "<color=#8C4005FF>Executioner</color>");
            OnTargetDead = new CustomStringOption(num++, "Executioner becomes on Target Dead",
                new[] {"Crew", "Jester"});

            /* THE GLITCH */
            TheGlitch =
                new CustomHeaderOption(num++, "<color=#00FF00FF>The Glitch</color>");
            MimicCooldownOption = new CustomNumberOption(num++, "Mimic Cooldown", 30, 10, 120, 2.5f, CooldownFormat);
            MimicDurationOption = new CustomNumberOption(num++, "Mimic Duration", 10, 1, 30, 1f, CooldownFormat);
            HackCooldownOption = new CustomNumberOption(num++, "Hack Cooldown", 30, 10, 120, 2.5f, CooldownFormat);
            HackDurationOption = new CustomNumberOption(num++, "Hack Duration", 10, 1, 30, 1f, CooldownFormat);
            GlitchKillCooldownOption =
                new CustomNumberOption(num++, "Glitch Kill Cooldown", 30, 10, 120, 2.5f, CooldownFormat);
            InitialGlitchKillCooldownOption =
                new CustomNumberOption(num++, "Initial Glitch Kill Cooldown", 10, 10, 120, 2.5f, CooldownFormat);
            GlitchHackDistanceOption =
                new CustomStringOption(num++, "Glitch Hack Distance", new[] {"Short", "Normal", "Long"});

            /* SHIFTER */
            Shifter =
                new CustomHeaderOption(num++, "<color=#999999FF>Shifter</color>");
            ShifterCd =
                new CustomNumberOption(num++, "Shifter Cooldown", 30f, 10f, 60f, 2.5f, CooldownFormat);
            WhoShifts = new CustomStringOption(num++,
                "Who gets the Shifter role on Shift", new[] {"Non-Impostors", "Crewmates only", "Nobody"});
            ShowShift = new CustomToggleOption(num++, "Show Shift", true);
            ShiftGlitch = new CustomToggleOption(num++, "Shift Glitch", false);
            ShifterCrewmate =
                new CustomToggleOption(num++, "Shifter Wins With Crew", false);

            /* PHANTOM */
            Phantom = new CustomHeaderOption(num++, "<color=#662962>Phantom</color>");
            PhantomSpawnInVent = new CustomToggleOption(num++, "Phantom spawns in vent", false);

            /* ASSASSIN */
            Assassin = new CustomHeaderOption(num++, "<color=#FF0000FF>Assassin</color>");
            AssassinKills = new CustomNumberOption(num++, "Number of Assassin Kills", 5, 1, 15, 1);
            AssassinCrewmateGuess = new CustomToggleOption(num++, "Assassin can Guess \"Crewmate\"", false);
            AssassinGuessChameleon = new CustomToggleOption(num++, "Assassin can Guess \"Chameleon\"", false);
            AssassinGuessNeutrals = new CustomToggleOption(num++, "Assassin can Guess Neutral roles", false);
            AssassinMultiKill = new CustomToggleOption(num++, "Assassin can kill more than once per meeting");
            AssassinShowKill = new CustomToggleOption(num++, "Show Assassination animation");

            /* CAMOUFLAGER */
            Camouflager =
                new CustomHeaderOption(num++, "<color=#FF0000FF>Camouflager</color>");
            CamouflagerCooldown =
                new CustomNumberOption(num++, "Camouflager Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            CamouflagerDuration =
                new CustomNumberOption(num++, "Camouflager Duration", 10, 5, 15, 1f, CooldownFormat);

            /* GRENADIER */
            Grenadier =
                new CustomHeaderOption(num++, "<color=#FF0000FF>Grenadier</color>");
            GrenadeCooldown =
                new CustomNumberOption(num++, "Flash Grenade Cooldown", 30, 10, 40, 2.5f, CooldownFormat);
            GrenadeDuration =
                new CustomNumberOption(num++, "Flash Grenade Duration", 10, 5, 15, 1f, CooldownFormat);
            GrenadierVent =
                new CustomToggleOption(num++, "Grenadier Can Vent", false);

            /* MINER */
            Miner = new CustomHeaderOption(num++, "<color=#FF0000FF>Miner</color>");
            MineCooldown =
                new CustomNumberOption(num++, "Mine Cooldown", 25, 10, 40, 2.5f, CooldownFormat);

            /* MORPHLING */
            Morphling =
                new CustomHeaderOption(num++, "<color=#FF0000FF>Morphling</color>");
            MorphlingCooldown =
                new CustomNumberOption(num++, "Morphling Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            MorphlingDuration =
                new CustomNumberOption(num++, "Morphling Duration", 10, 5, 15, 1f, CooldownFormat);

            /* POISONER */
            Poisoner =
                new CustomHeaderOption(num++, "<color=#FF0000FF>Poisoner</color>");
            PoisonerCooldown =
                new CustomNumberOption(num++, "Poisoner Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            PoisonerDuration =
                new CustomNumberOption(num++, "Poisoner Duration", 10, 1, 10, 1f, CooldownFormat);
            WhoGetsPoisonAlert =
                new CustomStringOption(num++, "Who gets poison alert",
                    new[] {"Poisoned", "Everyone", "Nobody"});

            /* SWOOPER */
            Swooper = new CustomHeaderOption(num++, "<color=#FF0000FF>Swooper</color>");
            SwoopCooldown =
                new CustomNumberOption(num++, "Swoop Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
            SwoopDuration =
                new CustomNumberOption(num++, "Swoop Duration", 10, 5, 15, 1f, CooldownFormat);

            /* UNDERDOG */
            Underdog = new CustomHeaderOption(num++, "<color=#FF0000FF>Underdog</color>");
            UnderdogPlayers = new CustomToggleOption(num++, "Underdog cooldown based on players dead");

            /* UNDERTAKER */
            Undertaker = new CustomHeaderOption(num++, "<color=#FF0000FF>Undertaker</color>");
            DragCooldown = new CustomNumberOption(num++, "Drag Cooldown", 25, 10, 40, 2.5f, CooldownFormat);
        }
    }
}