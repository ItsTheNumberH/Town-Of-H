using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;
using System.Linq;

namespace TownOfUs.ImpostorRoles.BlackmailerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static Sprite Blackmail => TownOfUs.BlackmailSprite;

        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Blackmailer)) return;
            var role = Role.GetRole<Blackmailer>(PlayerControl.LocalPlayer);
            if (role.BlackmailButton == null)
            {
                role.BlackmailButton = Object.Instantiate(__instance.KillButton, __instance.KillButton.transform.parent);
                role.BlackmailButton.graphic.enabled = true;
                role.BlackmailButton.GetComponent<AspectPosition>().DistanceFromEdge = TownOfUs.ButtonPosition;
                role.BlackmailButton.gameObject.SetActive(false);
            }

            role.BlackmailButton.GetComponent<AspectPosition>().Update();
            role.BlackmailButton.graphic.sprite = Blackmail;
            role.BlackmailButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);

            var notBlackmailed = PlayerControl.AllPlayerControls.ToArray().Where(
                player => role.Blackmailed?.PlayerId != player.PlayerId
            ).ToList();

            Utils.SetTarget(ref role.ClosestPlayer, role.BlackmailButton, GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance], notBlackmailed);

            role.BlackmailButton.SetCoolDown(role.BlackmailTimer(), CustomGameOptions.BlackmailCd);

            if (role.Blackmailed != null && !role.Blackmailed.Data.IsDead && !role.Blackmailed.Data.Disconnected)
            {
                role.Blackmailed.MyRend.material.SetFloat("_Outline", 1f);
                role.Blackmailed.MyRend.material.SetColor("_OutlineColor", new Color(0.3f, 0.0f, 0.0f));
                role.Blackmailed.nameText.color = new Color(0.3f, 0.0f, 0.0f);
            }
        }
    }
}