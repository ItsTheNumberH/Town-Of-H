namespace TownOfUs.Roles.Modifiers
{
    public class Volatile : Modifier
    {
        public Volatile(PlayerControl player) : base(player)
        {
            Name = "Volatile";
            TaskText = () => "You might see/hear things and lash out.";
            Color = Patches.Colors.Volatile;
            ModifierType = ModifierEnum.Volatile;
        }
    }
}