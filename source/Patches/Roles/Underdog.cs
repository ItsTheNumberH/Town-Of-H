using TownOfUs.ImpostorRoles.UnderdogMod;

namespace TownOfUs.Roles
{
    public class Underdog : Role
    {
        public Underdog(PlayerControl player) : base(player)
        {
            Name = "Underdog";
            ImpostorText = () => "Kill faster when the you're the lone impostor";
            TaskText = () => "Long kill cooldown while 2+ impostors alive\nShort cooldown when you're the only impostor alive.";
            Color = Patches.Colors.Impostor;
            RoleType = RoleEnum.Underdog;
            AddToRoleHistory(RoleType);
            Faction = Faction.Impostors;
        }

        public float MaxTimer() => PerformKill.LastImp() ? PlayerControl.GameOptions.KillCooldown - (CustomGameOptions.UnderdogKillBonus) : (PerformKill.IncreasedKC() ? PlayerControl.GameOptions.KillCooldown : PlayerControl.GameOptions.KillCooldown + (CustomGameOptions.UnderdogKillBonus));

        public void SetKillTimer()
        {
            Player.SetKillTimer(MaxTimer());
        }
    }
}
