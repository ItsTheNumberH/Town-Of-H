using System.Collections;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.NeutralRoles.CannibalMod
{
    public class Coroutine
    {
        private static readonly int BodyColor = Shader.PropertyToID("_BodyColor");
        private static readonly int BackColor = Shader.PropertyToID("_BackColor");

        public static IEnumerator EatCoroutine(DeadBody body, Cannibal role)
        {
            KillButtonTarget.SetTarget(DestroyableSingleton<HudManager>.Instance.KillButton, null, role);
            role.Player.SetKillTimer(PlayerControl.GameOptions.KillCooldown);
            var renderer = body.bodyRenderer;
            var backColor = renderer.material.GetColor(BackColor);
            var bodyColor = renderer.material.GetColor(BodyColor);
            var newColor = new Color(0f, 0f, 0f, 0f);
            for (var i = 0; i < 60; i++)
            {
                if (body == null) yield break;
                renderer.color = Color.Lerp(backColor, newColor, i / 60f);
                renderer.color = Color.Lerp(bodyColor, newColor, i / 60f);
                yield return null;
            }

            Object.Destroy(body.gameObject);
        }
    }
}