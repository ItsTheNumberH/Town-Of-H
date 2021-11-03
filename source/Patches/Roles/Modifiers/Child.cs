using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Child : Modifier
    {
        public Child(PlayerControl player) : base(player)
        {
            Name = "Child";
            TaskText = () => "You are tiny!";
            Color = new Color(1f, 0.5f, 0.5f, 1f);
            ModifierType = ModifierEnum.Child;
        }
    }
}