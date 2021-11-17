using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;

namespace TownOfUs.NeutralRoles.CannibalMod
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class HudManagerUpdate
    {
        public static ArrowBehaviour Arrow;
        public static DeadBody Target;
        public static void Postfix(HudManager __instance)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Cannibal)) return;
            if (PlayerControl.AllPlayerControls.Count <= 1) return;
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;
            var eatButton = __instance.KillButton;

            var role = Role.GetRole<Cannibal>(PlayerControl.LocalPlayer);
            if (role.EatButton == null)
            {
                role.EatButton = Object.Instantiate(__instance.KillButton, HudManager.Instance.transform);
                role.EatButton.renderer.enabled = true;
            }

            role.EatButton.gameObject.SetActive(!PlayerControl.LocalPlayer.Data.IsDead && !MeetingHud.Instance);
            var position = __instance.KillButton.transform.localPosition;
            role.EatButton.transform.localPosition = new Vector3(position.x - 1.3f,
                __instance.ReportButton.transform.localPosition.y, position.z);

            role.EatButton.renderer.sprite = TownOfUs.CannibalEat;

            var truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            var maxDistance = GameOptionsData.KillDistances[PlayerControl.GameOptions.KillDistance];
            var flag = (PlayerControl.GameOptions.GhostsDoTasks || !PlayerControl.LocalPlayer.Data.IsDead) &&
                       (!AmongUsClient.Instance || !AmongUsClient.Instance.IsGameOver) &&
                       PlayerControl.LocalPlayer.CanMove;
            var allocs = Physics2D.OverlapCircleAll(truePosition, 10,
                LayerMask.GetMask(new[] {"Players", "Ghost"}));
            DeadBody closestBody = null;
            var closestDistance = float.MaxValue;

            foreach (var collider2D in allocs)
            {
                if (!flag || PlayerControl.LocalPlayer.Data.IsDead || collider2D.tag != "DeadBody") continue;
                var component = collider2D.GetComponent<DeadBody>();
                var distance = Vector2.Distance(truePosition, component.TruePosition);
                if (distance <= 10 && Arrow == null) Target = component;
                else if (distance > 10 && Target == component) Target = null;
                if (!(distance <= maxDistance)) continue;  
                if (!(distance < closestDistance)) continue;

                closestBody = component;
                closestDistance = distance;
            }

            KillButtonTarget.SetTarget(eatButton, closestBody, role);

            if (CustomGameOptions.CannibalBodyArrows && Target != null && Arrow == null)
            {
                var gameObj = new GameObject();
                Arrow = gameObj.AddComponent<ArrowBehaviour>();
                gameObj.transform.parent = PlayerControl.LocalPlayer.gameObject.transform;
                var renderer = gameObj.AddComponent<SpriteRenderer>();
                renderer.sprite = TownOfUs.Arrow;
                renderer.color = role.Color;
                Arrow.image = renderer;
                gameObj.layer = 5;
            }
        }
    }
}