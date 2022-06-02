using System;
using System.Linq;
using HarmonyLib;

namespace TownOfUs.CrewmateRoles.MedicMod
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CmdReportDeadBody))]
    internal class BodyReportPatch
    {
        private static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] GameData.PlayerInfo info)
        {
            if (info == null) return;
            var matches = Murder.KilledPlayers.Where(x => x.PlayerId == info.PlayerId).ToArray();
            DeadPlayer killer = null;

            if (matches.Length > 0)
                killer = matches[0];

            if (killer == null)
                return;

            var isMedicAlive = __instance.Is(RoleEnum.Medic);
            var areReportsEnabled = CustomGameOptions.ShowReports;

            if (!isMedicAlive || !areReportsEnabled)
                return;

            var isUserMedic = PlayerControl.LocalPlayer.Is(RoleEnum.Medic);
            if (!isUserMedic)
                return;
            var br = new BodyReport
            {
                Killer = Utils.PlayerById(killer.KillerId),
                Reporter = __instance,
                Body = Utils.PlayerById(killer.PlayerId),
                KillAge = (float) (DateTime.UtcNow - killer.KillTime).TotalMilliseconds
            };

            var reportMsg = BodyReport.ParseBodyReport(br);

            if (string.IsNullOrWhiteSpace(reportMsg))
                return;

            if (DestroyableSingleton<HudManager>.Instance)
                // Send the message through chat only visible to the medic
                DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, reportMsg);
        }
    }
}