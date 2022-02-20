using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Giant : Modifier
    {
        public Giant(PlayerControl player) : base(player)
        {
            Name = "Giant";
            TaskText = () => "Big and slow";
            Color = new Color(1f, 0.5f, 0.5f, 1f);
            ModifierType = ModifierEnum.Giant;
        }
    }
}