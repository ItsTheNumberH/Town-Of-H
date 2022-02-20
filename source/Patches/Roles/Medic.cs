using System.Collections.Generic;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Medic : Role
    {
        public readonly List<GameObject> Buttons = new List<GameObject>();
        public Dictionary<int, string> LightDarkColors = new Dictionary<int, string>();
        public Medic(PlayerControl player) : base(player)
        {
            Name = "Medic";
            ImpostorText = () => "Create a shield to protect a crewmate";
            TaskText = () => "Protect a crewmate with a shield";
            Color = Patches.Colors.Medic;
            RoleType = RoleEnum.Medic;
            AddToRoleHistory(RoleType);
            ShieldedPlayer = null;

            LightDarkColors.Add(0, "darker");
            LightDarkColors.Add(1, "darker");
            LightDarkColors.Add(2, "darker");
            LightDarkColors.Add(3, "lighter");
            LightDarkColors.Add(4, "lighter");
            LightDarkColors.Add(5, "lighter");
            LightDarkColors.Add(6, "darker");
            LightDarkColors.Add(7, "lighter");
            LightDarkColors.Add(8, "darker");
            LightDarkColors.Add(9, "darker");
            LightDarkColors.Add(10, "lighter");
            LightDarkColors.Add(11, "lighter");
            LightDarkColors.Add(12, "darker");
            LightDarkColors.Add(13, "lighter");
            LightDarkColors.Add(14, "lighter");
            LightDarkColors.Add(15, "darker");
            LightDarkColors.Add(16, "darker");
            LightDarkColors.Add(17, "lighter");
            LightDarkColors.Add(18, "darker");
            LightDarkColors.Add(19, "darker");
            LightDarkColors.Add(20, "lighter");
            LightDarkColors.Add(21, "darker");
            LightDarkColors.Add(22, "lighter");
            LightDarkColors.Add(23, "lighter");
            LightDarkColors.Add(24, "lighter");
            LightDarkColors.Add(25, "lighter");
            LightDarkColors.Add(26, "lighter");
        }

        public PlayerControl ClosestPlayer;
        public bool UsedAbility { get; set; } = false;
        public PlayerControl ShieldedPlayer { get; set; }
        public PlayerControl exShielded { get; set; }
    }
}