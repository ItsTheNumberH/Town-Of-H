using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class ChildModifier : Modifier, IVisualAlteration
    {
        public static float SpeedFactor = 0.7f;
        
        public ChildModifier(PlayerControl player) : base(player)
        {
            Name = "Child";
            TaskText = () => "You are tiny!";
            Color = new Color(1f, 0.5f, 0.5f, 1f);
            ModifierType = ModifierEnum.Child;
        }

        public bool TryGetModifiedAppearance(out VisualAppearance appearance)
        {
            appearance = Player.GetDefaultAppearance();
            appearance.SpeedFactor = 1f;
            appearance.SizeFactor = new Vector3(0.45f, 0.45f, 0.45f);
            return true;
        }
    }
}