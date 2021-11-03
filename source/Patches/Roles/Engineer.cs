using UnityEngine;

namespace TownOfUs.Roles
{
    public class Engineer : Role
    {
        public Engineer(PlayerControl player) : base(player)
        {
            Name = "Engineer";
            ImpostorText = () => "Instantly fix a sabotage and use vents";
            TaskText = () => "You can use vents and fix a sabotage from anywhere.";
            Color = new Color(1f, 0.65f, 0.04f, 1f);
            RoleType = RoleEnum.Engineer;
        }

        public bool UsedThisRound { get; set; } = false;
    }
}