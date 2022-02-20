using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.ImpostorRoles.PoisonerMod
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CoStartMeeting))]
    class StartMeetingPatch
    {
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] GameData.PlayerInfo meetingTarget)
        {
            if (__instance == null)
            {
                return;
            }
            if (PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner))
            {
                var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
                if (PlayerControl.LocalPlayer != role.PoisonedPlayer)
                {
                    Utils.RpcMurderPlayer(PlayerControl.LocalPlayer, role.PoisonedPlayer);
                }
                return;
            }
        }
    }
}