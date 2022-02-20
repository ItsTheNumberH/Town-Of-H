using HarmonyLib;
using Hazel;
using TownOfUs.Roles;
using System.Linq;
using TownOfUs.CrewmateRoles.InvestigatorMod;
using TownOfUs.CrewmateRoles.SnitchMod;
using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.ImpostorRoles.TraitorMod
{
    [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
    public static class AirshipExileController_WrapUpAndSpawn
    {
        public static void Postfix(AirshipExileController __instance) => SetTraitor.ExileControllerPostfix(__instance);
    }
    
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
    public class SetTraitor
    {
        public static PlayerControl WillBeTraitor;

        public static void ExileControllerPostfix(ExileController __instance)
        {
            var exiled = __instance.exiled?.Object;
            var alives = PlayerControl.AllPlayerControls.ToArray()
                    .Where(x => !x.Data.IsDead && !x.Data.Disconnected).ToList();
            foreach (var player in alives)
            {
                if (player.Data.IsImpostor())
                {
                    return;
                }
            }
            if (PlayerControl.LocalPlayer.Data.IsDead || exiled == PlayerControl.LocalPlayer) return;
            if (alives.Count < CustomGameOptions.LatestSpawn) return;
            if (PlayerControl.LocalPlayer != WillBeTraitor) return;

            if (!PlayerControl.LocalPlayer.Is(RoleEnum.Traitor))
            {
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Snitch))
                {
                    var snitchRole = Role.GetRole<Snitch>(PlayerControl.LocalPlayer);
                    snitchRole.ImpArrows.DestroyAll();
                    snitchRole.SnitchArrows.DestroyAll();
                    snitchRole.SnitchTargets.Clear();
                    CompleteTask.Postfix(PlayerControl.LocalPlayer);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Investigator)) Footprint.DestroyAll(Role.GetRole<Investigator>(PlayerControl.LocalPlayer));

                if (PlayerControl.LocalPlayer.Is(RoleEnum.TimeLord))
                {
                    var timeLordRole = Role.GetRole<TimeLord>(PlayerControl.LocalPlayer);
                    Object.Destroy(timeLordRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Tracker))
                {
                    var trackerRole = Role.GetRole<Tracker>(PlayerControl.LocalPlayer);
                    trackerRole.DestroyAllArrows();
                    Object.Destroy(trackerRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Transporter))
                {
                    var transporterRole = Role.GetRole<Transporter>(PlayerControl.LocalPlayer);
                    Object.Destroy(transporterRole.UsesText);
                }

                if (PlayerControl.LocalPlayer.Is(RoleEnum.Veteran))
                {
                    var veteranRole = Role.GetRole<Veteran>(PlayerControl.LocalPlayer);
                    Object.Destroy(veteranRole.UsesText);
                }

                Role.RoleDictionary.Remove(PlayerControl.LocalPlayer.PlayerId);
                var role = new Traitor(PlayerControl.LocalPlayer);
                role.RegenTask();

                var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte) CustomRPC.TraitorSpawn, SendOption.Reliable, -1);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

                TurnImp(PlayerControl.LocalPlayer);
            }
        }

        public static void TurnImp(PlayerControl player)
        {
            player.Data.Role.TeamType = RoleTeamTypes.Impostor;
            RoleManager.Instance.SetRole(player, RoleTypes.Impostor);
            player.SetKillTimer(PlayerControl.GameOptions.KillCooldown);

            foreach (var player2 in PlayerControl.AllPlayerControls)
            {
                if (player2.Data.IsImpostor() && PlayerControl.LocalPlayer.Data.IsImpostor())
                {
                    player2.nameText.color = Patches.Colors.Impostor;
                }
            }

            if (CustomGameOptions.TraitorCanAssassin)
            {
                var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                    (byte)CustomRPC.SetAssassin, SendOption.Reliable, -1);
                writer2.Write(player.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer2);
            }

            Lights.SetLights();
        }

        public static void Postfix(ExileController __instance) => ExileControllerPostfix(__instance);
    }
}