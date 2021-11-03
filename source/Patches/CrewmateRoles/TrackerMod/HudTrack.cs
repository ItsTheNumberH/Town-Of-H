using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;

namespace TownOfUs.CrewmateRoles.TrackerMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class HudTrack
    {
        public static void Postfix(PlayerControl __instance)
        {
            UpdateTrackButton(__instance);
        }

        public static void UpdateTrackButton(PlayerControl __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Tracker)) return;
            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var trackButton = DestroyableSingleton<HudManager>.Instance.KillButton;

            var role = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);


            if (isDead || role.UsedTrack)
            {
                trackButton.gameObject.SetActive(false);
                trackButton.isActive = false;
            }
            else
            {
                trackButton.gameObject.SetActive(!MeetingHud.Instance);
                trackButton.isActive = !MeetingHud.Instance;
                trackButton.SetCoolDown(role.TrackerTimer(), CustomGameOptions.TrackerCd);

                var notTracked = PlayerControl.AllPlayerControls
                    .ToArray()
                    .Where(x => !role.Tracked.Contains(x.PlayerId))
                    .ToList();

                Utils.SetTarget(ref role.ClosestPlayer, trackButton, float.NaN, notTracked);
            }
        }
    }
}