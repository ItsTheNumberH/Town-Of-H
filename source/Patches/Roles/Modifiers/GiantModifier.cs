using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class GiantModifier : Modifier, IVisualAlteration
    {
        public static float SpeedFactor = 0.7f;
        
        public GiantModifier(PlayerControl player) : base(player)
        {
            Name = "Giant";
            TaskText = () => "Big and slow";
            Color = new Color(1f, 0.5f, 0.5f, 1f);
            ModifierType = ModifierEnum.Giant;
        }

        public bool TryGetModifiedAppearance(out VisualAppearance appearance)
        {
            appearance = Player.GetDefaultAppearance();
            appearance.SpeedFactor = SpeedFactor;
            appearance.SizeFactor = new Vector3(1.0f, 1.0f, 1.0f);
            return true;
        }
    }
}