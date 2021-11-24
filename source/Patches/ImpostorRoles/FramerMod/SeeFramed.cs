using HarmonyLib;
using Rewired;
using TownOfUs.ImpostorRoles.CamouflageMod;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.Patches.ImpostorRoles.FramerMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class SeeFramed
    {
        public static void Postfix(HudManager __instance)
        {
            if (
                PlayerControl.AllPlayerControls.Count <= 1
                || PlayerControl.LocalPlayer == null
                || PlayerControl.LocalPlayer.Data == null
                || !(PlayerControl.LocalPlayer.Data.IsImpostor || PlayerControl.LocalPlayer.Data.IsDead)
                || MeetingHud.Instance != null
            )
            {
                return;
            }

            foreach (Framer role in Role.GetRoles(RoleEnum.Framer))
            {
                PlayerControl framed = role.Framed;
                if (framed == null)
                {
                    return;
                }

                framed.nameText.transform.localPosition = new Vector3(0f, 2f, -0.5f);
                framed.nameText.color = Color.red;
                framed.nameText.text =
                    (CamouflageUnCamouflage.IsCamoed ? "" : framed.name) +
                    (role.TimeBeforeFramed > 0 ? " (Framing...)" : " (Framed)");
            }
        }
    }
}