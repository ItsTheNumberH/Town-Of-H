using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUs.CrewmateRoles.PsychicMod
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class AddButton
    {
        private static Sprite LighterSprite => TownOfUs.LighterSprite;
        public static Sprite DarkerSprite => TownOfUs.DarkerSprite;

        public static void GenButton(Psychic role, int index)
        {
            var colorButton = MeetingHud.Instance.playerStates[index].Buttons.transform.GetChild(0).gameObject;
            var newButton = Object.Instantiate(colorButton, MeetingHud.Instance.playerStates[index].transform);
            var renderer = newButton.GetComponent<SpriteRenderer>();

            PlayerControl playerControl = PlayerControl.AllPlayerControls.ToArray().FirstOrDefault(p => p.PlayerId == MeetingHud.Instance.playerStates[index].TargetPlayerId);

            if (role.LightDarkColors[playerControl.CurrentOutfit.ColorId] == "lighter") {
                renderer.sprite = LighterSprite;
            } else {
                renderer.sprite = DarkerSprite;
            }
            newButton.transform.position = colorButton.transform.position - new Vector3(-0.8f, 0.2f, -2f);
            newButton.transform.localScale *= 0.8f;
            newButton.layer = 5;
            newButton.transform.parent = colorButton.transform.parent.parent;
            role.Buttons.Add(newButton);
        }

        public static void Postfix(MeetingHud __instance)
        {
            foreach (var role in Role.GetRoles(RoleEnum.Psychic))
            {
                var psychic = (Psychic) role;
                psychic.Buttons.Clear();
            }
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Psychic)) return;
            if (PlayerControl.LocalPlayer.Data.IsDead) return;
            var psychicrole = Role.GetRole<Psychic>(PlayerControl.LocalPlayer);
            for (var i = 0; i < __instance.playerStates.Length; i++)
                GenButton(psychicrole, i);
        }
    }
}