using HarmonyLib;

namespace TownOfUs.Modifiers
{
    public class Bait
    {
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
        public class PlayerControl_MurderPlayer
        {
            public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
            {
                if (target.Is(ModifierEnum.Bait))
                      __instance.CmdReportDeadBody(GameData.Instance.GetPlayerById(target.PlayerId));
            }
        }
    }
}