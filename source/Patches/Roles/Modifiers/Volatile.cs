using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Volatile : Modifier
    {
        public Volatile(PlayerControl player) : base(player)
        {
            Name = "Volatile";
            TaskText = () => "You might see/hear things and lash out.";
            Color = new Color(1f, 0.65f, 0.04f, 1f);
            ModifierType = ModifierEnum.Volatile;
        }
    }
}