using System;
using UnityEngine;
using System.Linq;

namespace TownOfUs.Roles
{
    public class Grenadier : Role
    {
        public KillButtonManager _flashButton;
        public bool Enabled;
        public DateTime LastFlashed;
        public float TimeRemaining;
        Il2CppSystem.Collections.Generic.List<PlayerControl> closestPlayers = null;

        public Grenadier(PlayerControl player) : base(player)
        {
            Name = "Grenadier";
            ImpostorText = () => "Hinder the Crewmates Vision";
            TaskText = () => "Blind the crewmates to get sneaky kills";
            Color = Palette.ImpostorRed;
            RoleType = RoleEnum.Grenadier;
            Faction = Faction.Impostors;
        }

        public bool Flashed => TimeRemaining > 0f;

        public KillButtonManager FlashButton
        {
            get => _flashButton;
            set
            {
                _flashButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }
        public float FlashTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastFlashed;
            ;
            var num = CustomGameOptions.GrenadeCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
        public void Flash()
        {
            if (Enabled != true)
            {
                closestPlayers = FindClosestPlayers(Player);
            }
            Enabled = true;
            TimeRemaining -= Time.deltaTime;

            //To stop the scenario where the flash and sabotage are called at the same time.
            var system = ShipStatus.Instance.Systems[SystemTypes.Sabotage].Cast<SabotageSystemType>();
            var specials = system.specials.ToArray();
            var dummyActive = system.dummy.IsActive;
            var sabActive = specials.Any(s => s.IsActive);

            foreach (var player in closestPlayers)
            {
                try {
                    if (PlayerControl.LocalPlayer.PlayerId == player.PlayerId)
                    {
                        if (TimeRemaining > CustomGameOptions.GrenadeDuration - 0.5f && (!sabActive | dummyActive))
                        {
                            float fade = (TimeRemaining - CustomGameOptions.GrenadeDuration) * -2.0f;
                            if (!player.Data.IsImpostor && !player.Data.IsDead && !MeetingHud.Instance)
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = Color.Lerp((new Color(0.83f, 0.83f, 0.83f, 0f)), (new Color(0.83f, 0.83f, 0.83f, 1f)), fade);
                            }
                            else if ((player.Data.IsImpostor || player.Data.IsDead) && !MeetingHud.Instance)
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = Color.Lerp((new Color(0.83f, 0.83f, 0.83f, 0f)), (new Color(0.83f, 0.83f, 0.83f, 0.2f)), fade);
                                if (PlayerControl.LocalPlayer.Data.IsImpostor && MapBehaviour.Instance.infectedOverlay.SabSystem.Timer < 0.5f)
                                {
                                    MapBehaviour.Instance.infectedOverlay.SabSystem.Timer = 0.5f;
                                }
                            }
                            else
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 0f);
                            }
                        }
                        else if (TimeRemaining <= (CustomGameOptions.GrenadeDuration - 0.5f) && TimeRemaining >= 0.5f && (!sabActive | dummyActive))
                        {
                            if ((!player.Data.IsImpostor && !player.Data.IsDead) && !MeetingHud.Instance)
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 1f);
                            }
                            else if ((player.Data.IsImpostor || player.Data.IsDead) && !MeetingHud.Instance)
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 0.2f);
                                if (PlayerControl.LocalPlayer.Data.IsImpostor && MapBehaviour.Instance.infectedOverlay.SabSystem.Timer < 0.5f)
                                {
                                    MapBehaviour.Instance.infectedOverlay.SabSystem.Timer = 0.5f;
                                }
                            }
                            else
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 0f);
                            }
                        }
                        else if (TimeRemaining < 0.5f && (!sabActive | dummyActive))
                        {
                            float fade2 = (TimeRemaining * -2.0f) + 1.0f;
                            if ((!player.Data.IsImpostor && !player.Data.IsDead) && !MeetingHud.Instance)
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = Color.Lerp((new Color(0.83f, 0.83f, 0.83f, 1f)), (new Color(0.83f, 0.83f, 0.83f, 0f)), fade2);
                            }
                            else if ((player.Data.IsImpostor || player.Data.IsDead) && !MeetingHud.Instance)
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = Color.Lerp((new Color(0.83f, 0.83f, 0.83f, 0.2f)), (new Color(0.83f, 0.83f, 0.83f, 0f)), fade2);
                                if (PlayerControl.LocalPlayer.Data.IsImpostor && MapBehaviour.Instance.infectedOverlay.SabSystem.Timer < 0.5f)
                                {
                                    MapBehaviour.Instance.infectedOverlay.SabSystem.Timer = 0.5f;
                                }
                            }
                            else
                            {
                                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 0f);
                            }
                        }
                        else
                        {
                            ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                            DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 0f);
                            TimeRemaining = 0.0f;
                        }
                    }
                } catch {
                }
            }

            if (TimeRemaining > 0.5f)
            {
                if (PlayerControl.LocalPlayer.Data.IsImpostor && MapBehaviour.Instance.infectedOverlay.SabSystem.Timer < 0.5f)
                {
                    MapBehaviour.Instance.infectedOverlay.SabSystem.Timer = 0.5f;
                }
            }
        }

        public void UnFlash()
        {
            try {
                Enabled = false;
                LastFlashed = DateTime.UtcNow;
                ((Renderer)DestroyableSingleton<HudManager>.Instance.FullScreen).enabled = true;
                DestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0.83f, 0.83f, 0.83f, 0f);
            } catch {
            }
        }

        public static Il2CppSystem.Collections.Generic.List<PlayerControl> FindClosestPlayers(PlayerControl player)
        {
            Il2CppSystem.Collections.Generic.List<PlayerControl> playerControlList = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            float impostorLightMod = PlayerControl.GameOptions.ImpostorLightMod;
            Vector2 truePosition = player.GetTruePosition();
            Il2CppSystem.Collections.Generic.List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
            for (int index = 0; index < allPlayers.Count; ++index)
            {
                GameData.PlayerInfo playerInfo = allPlayers[index];
                if (!playerInfo.Disconnected)
                {
                    Vector2 vector2 = new Vector2(playerInfo.Object.GetTruePosition().x - truePosition.x, playerInfo.Object.GetTruePosition().y - truePosition.y);
                    float magnitude = ((Vector2) vector2).magnitude;
                    if (magnitude <= impostorLightMod*5)
                    {
                        PlayerControl playerControl = playerInfo.Object;
                        playerControlList.Add(playerControl);
                    }
                }
            }
            return playerControlList;
        }
    }
}