using System;
using HarmonyLib;
using TownOfUs.Roles;
using Object = UnityEngine.Object;

namespace TownOfUs.CrewmateRoles.ChameleonMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Chameleon))
            {
                var role = Role.GetRole<Chameleon>(PlayerControl.LocalPlayer);
                role.LastSwooped = DateTime.UtcNow;
            }
        }
    }
}