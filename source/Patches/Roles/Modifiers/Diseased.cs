using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Diseased : Modifier
    {
        public Diseased(PlayerControl player) : base(player)
        {
            Name = "Diseased";
            TaskText = () => "Killing you gives Impostors 3x cooldown";
            Color = Color.grey;
            ModifierType = ModifierEnum.Diseased;
        }
    }
}