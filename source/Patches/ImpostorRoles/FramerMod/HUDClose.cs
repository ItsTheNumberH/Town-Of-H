using System;
using HarmonyLib;
using TownOfUs.Roles;
using Object = UnityEngine.Object;

namespace TownOfUs.ImpostorRoles.FramerMod
{
    public enum FrameTarget
    {
        Self,
        Random //Currently not actually used as random. Just first alive player
    }
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    public static class HUDClose
    {
        public static void Postfix(Object obj)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Framer) || ExileController.Instance == null || obj != ExileController.Instance.gameObject) return;
            var role = Role.GetRole<Framer>(PlayerControl.LocalPlayer);
            role.LastFramed = DateTime.UtcNow;
        }
    }
}