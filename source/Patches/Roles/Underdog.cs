using TownOfUs.ImpostorRoles.UnderdogMod;

namespace TownOfUs.Roles
{
    public class Underdog : Role
    {
        public Underdog(PlayerControl player) : base(player)
        {
            Name = "Underdog";
            ImpostorText = () =>
                CustomGameOptions.UnderdogPlayers
                    ? "The fewer crewmates alive, the faster you kill"
                    : "You kill faster solo";
            TaskText = () =>
                CustomGameOptions.UnderdogPlayers
                    ? "Kill cooldown gets faster the fewer crewmates are alive"
                    : "Faster kill cooldown when solo, but longer with other impostors alive";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Underdog;
            Faction = Faction.Impostors;
        }

        public float MaxTimer() => PlayerControl.GameOptions.KillCooldown * (
            PerformKill.LastImp() ? 0.5f : 1.5f
        );
        public float MaxTimerRemainingPlayers() => PlayerControl.GameOptions.KillCooldown * (
            PerformKill.LastImpRemainingPlayers()
        );

        public void SetKillTimer()
        {
            if (CustomGameOptions.UnderdogPlayers) {
                Player.SetKillTimer(MaxTimerRemainingPlayers());
            } else {
                Player.SetKillTimer(MaxTimer());
            }
        }
    }
}
