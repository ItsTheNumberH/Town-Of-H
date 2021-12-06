using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace TownOfUs.Roles
{
    public class Freezer : Assassin
    {
        public KillButtonManager _freezeButton;
        public System.Collections.Generic.Dictionary<byte, float> freezeList = new System.Collections.Generic.Dictionary<byte, float>();
        public PlayerControl ClosestPlayer;
        public static Sprite _freezeSprite => TownOfUs.FreezeSprite;

        public Freezer(PlayerControl player) : base(player)
        {
            Name = "Freezer";
            ImpostorText = () => "Freeze the crewmates";
            TaskText = () => "Freeze a crewmate to stick them in place and kill them.";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Freezer;
            Faction = Faction.Impostors;
        }
        
        public KillButtonManager FreezeButton
        {
            get => _freezeButton;
            set
            {
                _freezeButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }
    }
}