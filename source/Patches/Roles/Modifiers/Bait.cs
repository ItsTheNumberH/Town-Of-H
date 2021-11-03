using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Bait : Modifier
    {
        public Bait(PlayerControl player) : base(player)
        {
            Name = "Bait";
            TaskText = () => "Killing you causes an instant self-report";
            Color = new Color(0f, 0.7f, 0.7f, 1f);
            ModifierType = ModifierEnum.Bait;
        }
    }
}