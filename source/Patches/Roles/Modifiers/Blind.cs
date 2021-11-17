using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Blind : Modifier
    {
        public Blind(PlayerControl player) : base(player)
        {
            Name = "Blind";
            TaskText = () => "Your report button does not light up";
            Color = new Color(1f, 1f, 1f, 1f);
            ModifierType = ModifierEnum.Blind;
        }
    }
}