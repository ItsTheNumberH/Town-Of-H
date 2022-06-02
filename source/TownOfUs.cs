using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using TownOfUs.CustomOption;
using TownOfUs.Patches;
using TownOfUs.RainbowMod;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using System.IO;

namespace TownOfUs
{
    [BepInPlugin(Id, "Town Of Us", VersionString)]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(SubmergedCompatibility.SUBMERGED_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    public class TownOfUs : BasePlugin
    {
        public const string Id = "com.slushiegoose.townofus";
        public const string VersionString = "6.1.1";
        public static System.Version Version = System.Version.Parse(VersionString);
        public static Sprite LogoBanner;
        public static Sprite LogoLobbyBanner;
        public static Sprite BirthdayCampFreddy;
        public static Sprite BirthdayBal;
        public static Sprite BirthdayH;
        public static Sprite BirthdayCtri;
        public static Sprite BirthdayChaos;
        public static Sprite ChristmasLobby;
        public static Sprite JanitorClean;
        public static Sprite EngineerFix;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Footprint;
        public static Sprite Rewind;
        public static Sprite MedicSprite;
        public static Sprite SeerSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite Arrow;
        public static Sprite Abstain;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite DouseSprite;
        public static Sprite IgniteSprite;
        public static Sprite ReviveSprite;
        public static Sprite ButtonSprite;
        public static Sprite CycleBackSprite;
        public static Sprite CycleForwardSprite;
        public static Sprite GuessSprite;
        public static Sprite DragSprite;
        public static Sprite DropSprite;
        public static Sprite FlashSprite;
        public static Sprite AlertSprite;
        public static Sprite RememberSprite;
        public static Sprite TrackSprite;
        public static Sprite PoisonSprite;
        public static Sprite PoisonedSprite;
        public static Sprite TransportSprite;
        public static Sprite MediateSprite;
        public static Sprite VestSprite;
        public static Sprite ProtectSprite;
        public static Sprite BlackmailSprite;
        public static Sprite BlackmailLetterSprite;
        public static Sprite BlackmailOverlaySprite;
        public static Sprite LighterSprite;
        public static Sprite DarkerSprite;

        public static Sprite SettingsButtonSprite;
        public static Sprite CrewSettingsButtonSprite;
        public static Sprite NeutralSettingsButtonSprite;
        public static Sprite ImposterSettingsButtonSprite;
        public static Sprite ModifierSettingsButtonSprite;
        public static Sprite ToUBanner;
        public static Sprite UpdateTOUButton;
        public static Sprite UpdateSubmergedButton;

        public static Sprite HorseEnabledImage;
        public static Sprite HorseDisabledImage;
        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);

        private static DLoadImage _iCallLoadImage;


        private Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }
        public static class AssetLoader
        {
            private static readonly Assembly allulCustomLobby = Assembly.GetExecutingAssembly();

            public static void LoadAssets() {

                var resourceStreamLobby = allulCustomLobby.GetManifestResourceStream("TownOfUs.Resources.allulcustomlobby");
                var assetBundleLobby = AssetBundle.LoadFromMemory(resourceStreamLobby.ReadFully());

                CustomMain.customAssets.customLobby = assetBundleLobby.LoadAsset<GameObject>("allul_customLobby.prefab").DontDestroy();

                assetBundleLobby.Unload(false);

            }
        }
        public override void Load()
        {
            AssetLoader.LoadAssets();

            _harmony = new Harmony("com.slushiegoose.townofus");

            Generate.GenerateAll();
            //Custom Logo and Lobby baners
            LogoLobbyBanner = CreateSprite("TownOfUs.Resources.TownOfHLobbyBanner.png");
            BirthdayCampFreddy = CreateSprite("TownOfUs.Resources.BirthdayCampFreddy.png");
            BirthdayBal = CreateSprite("TownOfUs.Resources.BirthdayBal.png");
            BirthdayCtri = CreateSprite("TownOfUs.Resources.BirthdayCtri.png");
            BirthdayChaos = CreateSprite("TownOfUs.Resources.BirthdayChaos.png");
            BirthdayH = CreateSprite("TownOfUs.Resources.BirthdayH.png");
            ChristmasLobby = CreateSprite("TownOfUs.Resources.ChristmasLobby.png");

            JanitorClean = CreateSprite("TownOfUs.Resources.Janitor.png");
            EngineerFix = CreateSprite("TownOfUs.Resources.Engineer.png");
            SwapperSwitch = CreateSprite("TownOfUs.Resources.SwapperSwitch.png");
            SwapperSwitchDisabled = CreateSprite("TownOfUs.Resources.SwapperSwitchDisabled.png");
            Footprint = CreateSprite("TownOfUs.Resources.Footprint.png");
            Rewind = CreateSprite("TownOfUs.Resources.Rewind.png");
            MedicSprite = CreateSprite("TownOfUs.Resources.Medic.png");
            SeerSprite = CreateSprite("TownOfUs.Resources.Seer.png");
            SampleSprite = CreateSprite("TownOfUs.Resources.Sample.png");
            MorphSprite = CreateSprite("TownOfUs.Resources.Morph.png");
            Arrow = CreateSprite("TownOfUs.Resources.Arrow.png");
            Abstain = CreateSprite("TownOfUs.Resources.Abstain.png");
            MineSprite = CreateSprite("TownOfUs.Resources.Mine.png");
            SwoopSprite = CreateSprite("TownOfUs.Resources.Swoop.png");
            DouseSprite = CreateSprite("TownOfUs.Resources.Douse.png");
            IgniteSprite = CreateSprite("TownOfUs.Resources.Ignite.png");
            ReviveSprite = CreateSprite("TownOfUs.Resources.Revive.png");
            ButtonSprite = CreateSprite("TownOfUs.Resources.Button.png");
            DragSprite = CreateSprite("TownOfUs.Resources.Drag.png");
            DropSprite = CreateSprite("TownOfUs.Resources.Drop.png");
            CycleBackSprite = CreateSprite("TownOfUs.Resources.CycleBack.png");
            CycleForwardSprite = CreateSprite("TownOfUs.Resources.CycleForward.png");
            GuessSprite = CreateSprite("TownOfUs.Resources.Guess.png");
            FlashSprite = CreateSprite("TownOfUs.Resources.Flash.png");
            AlertSprite = CreateSprite("TownOfUs.Resources.Alert.png");
            RememberSprite = CreateSprite("TownOfUs.Resources.Remember.png");
            TrackSprite = CreateSprite("TownOfUs.Resources.Track.png");
            PoisonSprite = CreateSprite("TownOfUs.Resources.Poison.png");
            PoisonedSprite = CreateSprite("TownOfUs.Resources.Poisoned.png");
            TransportSprite = CreateSprite("TownOfUs.Resources.Transport.png");
            MediateSprite = CreateSprite("TownOfUs.Resources.Mediate.png");
            VestSprite = CreateSprite("TownOfUs.Resources.Vest.png");
            ProtectSprite = CreateSprite("TownOfUs.Resources.Protect.png");
            BlackmailSprite = CreateSprite("TownOfUs.Resources.Blackmail.png");
            BlackmailLetterSprite = CreateSprite("TownOfUs.Resources.BlackmailLetter.png");
            BlackmailOverlaySprite = CreateSprite("TownOfUs.Resources.BlackmailOverlay.png");
            LighterSprite = CreateSprite("TownOfUs.Resources.Lighter.png");
            DarkerSprite = CreateSprite("TownOfUs.Resources.Darker.png");

            SettingsButtonSprite = CreateSprite("TownOfUs.Resources.SettingsButton.png");
            ToUBanner = CreateSprite("TownOfUs.Resources.TownOfH.png");
            UpdateTOUButton = CreateSprite("TownOfUs.Resources.UpdateToUButton.png");
            UpdateSubmergedButton = CreateSprite("TownOfUs.Resources.UpdateSubmergedButton.png");

            CrewSettingsButtonSprite = CreateSprite("TownOfUs.Resources.CrewmateSettings.png");
            NeutralSettingsButtonSprite = CreateSprite("TownOfUs.Resources.NeutralSettings.png");
            ImposterSettingsButtonSprite = CreateSprite("TownOfUs.Resources.ImpostorSettings.png");
            ModifierSettingsButtonSprite = CreateSprite("TownOfUs.Resources.ModifierSettings.png");

            HorseEnabledImage = CreateSprite("TownOfUs.Resources.HorseOn.png");
            HorseDisabledImage = CreateSprite("TownOfUs.Resources.HorseOff.png");

            PalettePatch.Load();
            ClassInjector.RegisterTypeInIl2Cpp<RainbowBehaviour>();

            Ip = Config.Bind("Custom", "Ipv4 or Hostname", "127.0.0.1");
            Port = Config.Bind("Custom", "Port", (ushort) 22023);
            var defaultRegions = ServerManager.DefaultRegions.ToList();
            var ip = Ip.Value;
            if (Uri.CheckHostName(Ip.Value).ToString() == "Dns")
                foreach (var address in Dns.GetHostAddresses(Ip.Value))
                {
                    if (address.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    ip = address.ToString();
                    break;
                }

            ServerManager.DefaultRegions = defaultRegions.ToArray();

            _harmony.PatchAll();
            SubmergedCompatibility.Initialize();
        }

        public static Sprite CreateSprite(string name)
        {
            var pixelsPerUnit = 100f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = GUIExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }

        public static void LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            _iCallLoadImage ??= IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
            var il2CPPArray = (Il2CppStructArray<byte>) data;
            _iCallLoadImage.Invoke(tex.Pointer, il2CPPArray.Pointer, markNonReadable);
        }

        private delegate bool DLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);

        public static AudioClip loadAudioClipFromResources(string path, string sfxName = "")
        {
            if (CustomGameOptions.SFXOn) {
                try {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream(path);
                    var byteAudio = new byte[stream.Length];
                    _ = stream.Read(byteAudio, 0, (int)stream.Length);
                    float[] samples = new float[byteAudio.Length / 4];
                    int offset;
                    for (int i = 0; i < samples.Length; i++)
                    {
                        offset = i * 4;
                        samples[i] = (float)BitConverter.ToInt32(byteAudio, offset) / Int32.MaxValue;
                    }
                    int channels = 2;
                    int sampleRate = 48000;
                    AudioClip audioClip = AudioClip.Create(sfxName, samples.Length, channels, sampleRate, false);
                    audioClip.SetData(samples, 0);
                    return audioClip;
                } catch {
                }
            }
            return null;
        }
    }
    public static class CustomMain
    {
        public static CustomAssets customAssets = new CustomAssets();
    }

    public class CustomAssets
    {
        public GameObject customLobby;
    }
}
