namespace TownOfUs.Roles
{
    public class Traitor : Role
    {

        public Traitor(PlayerControl player) : base(player)
        {
            Name = "Traitor";
            ImpostorText = () => "";
            TaskText = () => "Betray the Crewmates!";
            Color = Patches.Colors.Impostor;
            RoleType = RoleEnum.Traitor;
            AddToRoleHistory(RoleType);
            Faction = Faction.Impostors;
        }
    }
}