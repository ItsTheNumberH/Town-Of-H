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

        [HarmonyPatch]
        private static class GameOptionsDataPatch
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                return typeof(GameOptionsData).GetMethods(typeof(string), typeof(int));
            }

            private static void Postfix(ref string __result)
            {
                var builder = new StringBuilder("");

                foreach (var option in CustomOption.CustomOption.AllOptions)
                {
                    if (option.Name == "Crewmate Roles") {
                        builder.Append("<color=#00FF00FF>Version " + TownOfUs.VersionString + "</color>");
                    }
                    else if (option.Name == "Custom Game Settings") {
                        builder.AppendLine("");
                        builder.Append(new StringBuilder(__result));
                    }
                    if (option.Type == CustomOptionType.Button) continue;
                    if (option.Type == CustomOptionType.Header) builder.AppendLine($"\n{option.Name}");
                    else if (option.Indent) builder.AppendLine($"     {option.Name}: {option}");
                    else builder.AppendLine($"{option.Name}: {option}");
                }
                __result = builder.ToString();
                __result = $"<size=1.25>{__result}</size>";
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