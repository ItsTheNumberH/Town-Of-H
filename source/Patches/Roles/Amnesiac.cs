namespace TownOfUs.Roles
{
    public class Amnesiac : Role
    {
        public Amnesiac(PlayerControl player) : base(player)
        {
            Name = "Amnesiac";
            ImpostorText = () => "Remember a role of a deceased player";
            TaskText = () => "Find a dead body to remember who you were.\nFake Tasks:";
            Color = Patches.Colors.Amnesiac;
            RoleType = RoleEnum.Amnesiac;
            AddToRoleHistory(RoleType);
            Faction = Faction.Neutral;
        }

        public DeadBody CurrentTarget;

        public void Loses()
        {
            LostByRPC = true;
        }

        protected override void IntroPrefix(IntroCutscene._CoBegin_d__18 __instance)
        {
            var amnesiacTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            amnesiacTeam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = amnesiacTeam;
        }
    }
}