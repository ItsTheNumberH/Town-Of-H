using UnityEngine;

namespace TownOfUs.Roles
{
    public class Impostor : Role
    {
        public Impostor(PlayerControl player) : base(player)
        {
            Name = "Impostor";
            ImpostorText = () => "A normal impostor";
            Hidden = true;
            Faction = Faction.Impostors;
            RoleType = RoleEnum.Impostor;
            Color = Palette.ImpostorRed;
        }
    }

    public class Crewmate : Role
    {
        public Crewmate(PlayerControl player) : base(player)
        {
            Name = "Crewmate";
            ImpostorText = () => "Just a crewmate";
            Hidden = true;
            Faction = Faction.Crewmates;
            RoleType = RoleEnum.Crewmate;
            Color = Color.white;
        }
    }
}