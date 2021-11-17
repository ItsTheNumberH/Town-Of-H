using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class BigBoi : Modifier
    {
        public BigBoi(PlayerControl player) : base(player)
        {
            Name = "Giant";
            TaskText = () => "Big and slow";
            Color = new Color(0.83f, 0.69f, 0.22f, 1f);
            ModifierType = ModifierEnum.BigBoi;
        }
    }
}