using UnityEngine;

namespace TownOfUs.Roles
{
    public class Altruist : Role
    {
        public bool CurrentlyReviving;
        public DeadBody CurrentTarget;

        public bool ReviveUsed;

        public Altruist(PlayerControl player) : base(player)
        {
            Name = "Altruist";
            ImpostorText = () => "Sacrifice yourself to revive another";
            TaskText = () => "Revive a dead body by sacrificing yourself.";
            Color = new Color(0.4f, 0f, 0f, 1f);
            RoleType = RoleEnum.Altruist;
        }
    }
}