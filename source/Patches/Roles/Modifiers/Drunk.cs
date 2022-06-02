namespace TownOfUs.Roles.Modifiers
{
    public class Drunk : Modifier
    {
        public Drunk(PlayerControl player) : base(player)
        {
            Name = "Drunk";
            TaskText = () => "Inverted controls";
            Color = Patches.Colors.Drunk;
            ModifierType = ModifierEnum.Drunk;
        }
    }
}