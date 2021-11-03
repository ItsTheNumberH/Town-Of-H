using HarmonyLib;

namespace TownOfUs.CrewmateRoles.InvestigatorMod
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__14), nameof(IntroCutscene._CoBegin_d__14.MoveNext))]
    public static class StartGame
    {
        public static void Postfix(IntroCutscene._CoBegin_d__14 __instance)
        {
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator)) {
                AddPrints.GameStarted = true;
            }
        }
    }
}