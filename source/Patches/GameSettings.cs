using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using Reactor.Extensions;
using TownOfUs.CustomOption;
using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs
{
    [HarmonyPatch]
    public static class GameSettings
    {
        public static bool AllOptions;

        [HarmonyPatch] //ToHudString
        private static class GameOptionsDataPatch
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                return typeof(GameOptionsData).GetMethods(typeof(string), typeof(int));
            }

            private static void Postfix(ref string __result)
            {
                var builder = new StringBuilder(AllOptions ? __result : "");

                foreach (var option in CustomOption.CustomOption.AllOptions)
                {
                    if (option.Name == "Custom Game Settings" && !AllOptions) break;
                    if (option.Type == CustomOptionType.Button) continue;
                    if (option.Type == CustomOptionType.Header) builder.AppendLine($"\n{option.Name}");
                    else if (option.Indent) builder.AppendLine($"     {option.Name}: {option}");
                    else builder.AppendLine($"{option.Name}: {option}");
                }

                __result = builder.ToString();

                __result = __result.Insert(__result.IndexOf('\n'), "<color=#00FF00FF>Version " + TownOfUs.VersionString + "</color>");

                __result = $"<size=1.25>{__result}</size>";
            }
        }

        [HarmonyPatch(typeof(LobbyBehaviour), nameof(LobbyBehaviour.FixedUpdate))]
        private static class LobbyBehaviourPatch
        {
            private static void Postfix()
            {
                if (Input.GetKeyInt(KeyCode.Tab)) AllOptions = !AllOptions;
            }
        }

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
        public static class Update
        {
            public static void Postfix(ref GameOptionsMenu __instance)
            {
                __instance.GetComponentInParent<Scroller>().YBounds.max = 98f;
            }
        }
    }
}