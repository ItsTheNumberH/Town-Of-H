using HarmonyLib;
using Reactor;
using TownOfUs.Patches;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.NeutralRoles.AmnesiacMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static ArrowBehaviour Arrow;
        public static DeadBody Target;
        public static void Postfix(HudManager __instance)
        {
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Amnesiac)) return;

            var role = Role.GetRole<Amnesiac>(PlayerControl.LocalPlayer);

            var data = PlayerControl.LocalPlayer.Data;
            var isDead = data.IsDead;
            var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            var flag = (PlayerControl.GameOptions.GhostsDoTasks || !data.IsDead) &&
                       (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                       PlayerControl.LocalPlayer.CanMove;
            var allocs = Physics2D.OverlapCircleAll(truePosition, maxDistance,
                LayerMask.GetMask(new[] { "Players", "Ghost" }));

            var killButton = __instance.KillButton;
            DeadBody closestBody = null;
            var closestDistance = float.MaxValue;

            foreach (var collider2D in allocs)
            {
                if (!flag || isDead || collider2D.tag != "DeadBody") continue;
                var component = collider2D.GetComponent<DeadBody>();


                if (!(Vector2.Distance(truePosition, component.TruePosition) <=
                      maxDistance)) continue;

                var distance = Vector2.Distance(truePosition, component.TruePosition);
                if (!(distance < closestDistance)) continue;
                closestBody = component;
                closestDistance = distance;
            }

            if (isDead)
            {
                killButton.gameObject.SetActive(false);
                // killButton.isActive = false;
            }
            else
            {
                killButton.gameObject.SetActive(!MeetingHud.Instance);
                // killButton.isActive = !MeetingHud.Instance;
            }

            KillButtonTarget.SetTarget(killButton, closestBody, role);
            __instance.KillButton.SetCoolDown(0f, 1f);
            
            foreach (var body in Object.FindObjectsOfType<DeadBody>()) {
                Target = body;
                if (Arrow == null)
                {
                    var gameObj = new GameObject();
                    Arrow = gameObj.AddComponent<ArrowBehaviour>();
                    gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                    var arrowRenderer = gameObj.AddComponent<SpriteRenderer>();
                    arrowRenderer.sprite = TownOfUs.Arrow;
                    arrowRenderer.color = role.Color;
                    Arrow.image = arrowRenderer;
                    gameObj.layer = 5;
                }
                break;
            }
        }
    }
}