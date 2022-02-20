using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine.UI;

namespace TownOfUs.CrewmateRoles.VigilanteMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Confirm))]
    public class ShowHideButtonsVigi
    {
        public static void HideButtonsVigi(Vigilante role)
        {
            foreach (var (_, (cycle, guess, guessText)) in role.Buttons)
            {
                if (cycle == null) continue;
                cycle.SetActive(false);
                guess.SetActive(false);
                guessText.gameObject.SetActive(false);

                cycle.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                guess.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
                role.GuessedThisMeeting = true;
            }
        }

        public static void HideSingle(
            Vigilante role,
            byte targetId,
            bool killedSelf
        )
        {
            if (
                killedSelf ||
                role.RemainingKills == 0 ||
                !CustomGameOptions.VigilanteMultiKill
            )
            {
                HideButtonsVigi(role);
                return;
            }

            var (cycle, guess, guessText) = role.Buttons[targetId];
            if (cycle == null) return;
            cycle.SetActive(false);
            guess.SetActive(false);
            guessText.gameObject.SetActive(false);

            cycle.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            guess.GetComponent<PassiveButton>().OnClick = new Button.ButtonClickedEvent();
            role.Buttons[targetId] = (null, null, null);
            role.Guesses.Remove(targetId);
        }


        public static void Prefix(MeetingHud __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Vigilante)) return;
            var retributionist = Role.GetRole<Vigilante>(PlayerControl.LocalPlayer);
            //HideButtonsVigi(retributionist);
        }
    }
}
