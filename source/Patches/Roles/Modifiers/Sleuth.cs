using UnityEngine;
using System.Collections.Generic;

namespace TownOfUs.Roles.Modifiers
{
    public class Sleuth : Modifier
    {
        public List<byte> Reported = new List<byte>();
        public Sleuth(PlayerControl player) : base(player)
        {
            Name = "Sleuth";
            TaskText = () => "Discover the roles of bodies you report";
            Color = new Color(0.5f, 0.2f, 0.2f);
            ModifierType = ModifierEnum.Sleuth;
        }
    }
}