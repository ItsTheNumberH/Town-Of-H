using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using TownOfUs.Extensions;
using TownOfUs.Roles;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngineInternal;

namespace TownOfUs.ImpostorRoles.FreezerMod {
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class freezeBreak {
        public static void Postfix(PlayerControl __instance) {
            if (!__instance.Is(RoleEnum.Freezer)) {
                return;
            }
            
            Freezer role = Role.GetRole<Freezer>(__instance);
            if (LobbyBehaviour.Instance || MeetingHud.Instance) {
                return;
            }


            var breakList = new Queue<byte>();
            foreach (var freeze in role.freezeList) {
                if (GameData.Instance.GetPlayerById(freeze.Key).IsDead) {
                    continue;
                }
                PlayerControl closestPlayer = null;
                System.Collections.Generic.List<PlayerControl> targets = PlayerControl.AllPlayerControls.ToArray()
                    .ToList().FindAll(x =>
                        x.PlayerId != role.Player.PlayerId && x.PlayerId != freeze.Key);
                if (Utils.SetClosestPlayerToPlayer(GameData.Instance.GetPlayerById(freeze.Key)._object, ref closestPlayer,
                    0.8f, targets
                )) {
                    breakList.Enqueue(freeze.Key);
                }
            }

            foreach (var breakQueue in breakList) {
                role.freezeList.Remove(breakQueue);
                Utils.RpcMurderPlayer(__instance, GameData.Instance.GetPlayerById(breakQueue)._object);
            }
        }
    }
    
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class freezeCantMove {
        public static void Postfix(PlayerControl __instance) {
            if (!__instance.Is(RoleEnum.Freezer)) {
                return;
            }
            Freezer role = Role.GetRole<Freezer>(__instance);

            var breakList = new Queue<byte>();
            var keys = role.freezeList.Keys.ToArray();
            foreach (var key in keys) {
                if (GameData.Instance.GetPlayerById(key).IsDead) {
                    breakList.Enqueue(key);
                    continue;
                }
                if (key != PlayerControl.LocalPlayer.PlayerId) {
                    continue;
                }
                role.freezeList[key] += Time.fixedDeltaTime;
                PlayerControl.LocalPlayer.moveable = false;
                PlayerControl.LocalPlayer.NetTransform.Halt();
                if (role.freezeList[key] >= CustomGameOptions.FreezeBreakTime) {
                    breakList.Enqueue(key);
                }
            }
            foreach (var breakQueue in breakList) {
                Utils.RpcMurderPlayer(__instance, GameData.Instance.GetPlayerById(breakQueue)._object);
                role.freezeList.Remove(breakQueue);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CoStartMeeting))]
    class StartMeetingPatch {
        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)]GameData.PlayerInfo meetingTarget) {
            if (__instance == null) {
                return;
            }
            foreach (var role in Role.GetRoles(RoleEnum.Freezer)) {
                if (((Freezer)role).freezeList.Count <= 0) continue;
                foreach (var (key, _) in ((Freezer)role).freezeList) {
                    role.Player.MurderPlayer(GameData.Instance.GetPlayerById(key)._object);
                }
                ((Freezer)role).freezeList.Clear();
            }
        }
    }
}