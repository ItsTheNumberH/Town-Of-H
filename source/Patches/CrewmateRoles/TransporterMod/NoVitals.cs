using HarmonyLib;
using UnityEngine;

namespace TownOfUs.CrewmateRoles.TransporterMod
{
    [HarmonyPatch(typeof(VitalsMinigame), nameof(VitalsMinigame.Begin))]
    public class NoVitals
    {
        public static bool Prefix(VitalsMinigame __instance)
        {
            if (CustomGameOptions.TransporterVitals) return true;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter) && !PlayerControl.LocalPlayer.Data.IsDead)
            {
                Object.Destroy(__instance.gameObject);
                return false;
            }

            return true;
        }
    }
}