using System;
using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;
using Object = UnityEngine.Object;

namespace TownOfUs.NeutralRoles.JuggernautMod
{
    [HarmonyPatch(typeof(Object), nameof(Object.Destroy), typeof(Object))]
    internal class MeetingExiledEnd
    {
        private static void Prefix(Object obj)
        {
            if (ExileController.Instance != null && obj == ExileController.Instance.gameObject)
            {
                var juggernaut = Role.AllRoles.FirstOrDefault(x => x.RoleType == RoleEnum.Juggernaut);
                if (juggernaut != null)
                {
                    ((Juggernaut)juggernaut).LastKill = DateTime.UtcNow;
                }
            }
        }
    }
}