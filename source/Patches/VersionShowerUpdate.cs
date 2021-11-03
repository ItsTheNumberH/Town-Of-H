using HarmonyLib;

namespace TownOfUs
{
    [HarmonyPriority(Priority.VeryHigh)]
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class VersionShowerUpdate
    {
        public static void Postfix(VersionShower __instance)
        {
            var text = __instance.text;
            text.text += " - <color=#00FF00FF>Town of Us -H " + TownOfUs.VersionString + "</color>";
        }
    }
}
