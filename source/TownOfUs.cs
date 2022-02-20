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
using TownOfUs.Patches.CustomHats;
using TownOfUs.RainbowMod;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace TownOfUs
{
    [BepInPlugin(Id, "Town Of Us", VersionString)]
    [BepInDependency(ReactorPlugin.Id)]
    public class TownOfUs : BasePlugin
    {
        public const string Id = "com.slushiegoose.townofus";
        
        public const string VersionString = "5.0.4";
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
        public static Sprite NormalKill;
        public static Sprite MedicSprite;
        public static Sprite SeerSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite Camouflage;
        public static Sprite Arrow;
        public static Sprite Abstain;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite DouseSprite;
        public static Sprite IgniteSprite;
        public static Sprite ReviveSprite;
        public static Sprite ButtonSprite;
        public static Sprite CycleSprite;
        public static Sprite GuessSprite;
        public static Sprite DragSprite;
        public static Sprite DropSprite;
        public static Sprite FlashSprite;
        public static Sprite SettingsButtonSprite;
        public static Sprite LighterSprite;
        public static Sprite DarkerSprite;
        public static Vector3 ButtonPosition { get; private set; } = new Vector3(2.6f, 0.7f, -9f);
        public static Sprite AlertSprite;
        public static Sprite RememberSprite;
        public static Sprite TrackSprite;
        public static Sprite PoisonSprite;
        public static Sprite PoisonedSprite;
        public static Sprite TransportSprite;
        public static Sprite ToUBanner;

        private static DLoadImage _iCallLoadImage;

        public static bool activatedSensei = false;

        public static bool updatedSenseiMinimap = false;
        public static bool updatedSenseiAdminmap = false;

        private Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }

        public static class AssetLoader
        {
            private static readonly Assembly allulCustomBundle = Assembly.GetExecutingAssembly();
            private static readonly Assembly allulCustomLobby = Assembly.GetExecutingAssembly();
            private static readonly Assembly allulCustomMusic = Assembly.GetExecutingAssembly();
            private static readonly Assembly allulCustomMap = Assembly.GetExecutingAssembly();

            public static void LoadAssets() {

                // Custom Lobby Assets
                var resourceStreamLobby = allulCustomLobby.GetManifestResourceStream("TownOfUs.Resources.allulcustomlobby");
                var assetBundleLobby = AssetBundle.LoadFromMemory(resourceStreamLobby.ReadFully());

                CustomMain.customAssets.customLobby = assetBundleLobby.LoadAsset<GameObject>("allul_customLobby.prefab").DontDestroy();

                // Custom Map Assets
                var resourceStreamMap = allulCustomMap.GetManifestResourceStream("TownOfUs.Resources.allulcustommap");
                var assetBundleMap = AssetBundle.LoadFromMemory(resourceStreamMap.ReadFully());

                CustomMain.customAssets.customMap = assetBundleMap.LoadAsset<GameObject>("HalconUI.prefab").DontUnload();
                CustomMain.customAssets.customMinimap = assetBundleMap.LoadAsset<GameObject>("Minimap.prefab").DontUnload();
                CustomMain.customAssets.customComms = assetBundleMap.LoadAsset<GameObject>("new_comms.prefab").DontUnload();

                //assetBundleBundle.Unload(false);
                assetBundleLobby.Unload(false);
                assetBundleMap.Unload(false);

            }
        }
        public override void Load()
        {
            AssetLoader.LoadAssets();
            System.Console.WriteLine("000.000.000.000/000000000000000000");

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
            NormalKill = CreateSprite("TownOfUs.Resources.NormalKill.png");
            MedicSprite = CreateSprite("TownOfUs.Resources.Medic.png");
            SeerSprite = CreateSprite("TownOfUs.Resources.Seer.png");
            SampleSprite = CreateSprite("TownOfUs.Resources.Sample.png");
            MorphSprite = CreateSprite("TownOfUs.Resources.Morph.png");
            Camouflage = CreateSprite("TownOfUs.Resources.Camouflage.png");
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
            CycleSprite = CreateSprite("TownOfUs.Resources.Cycle.png");
            GuessSprite = CreateSprite("TownOfUs.Resources.Guess.png");
            FlashSprite = CreateSprite("TownOfUs.Resources.Flash.png");
            SettingsButtonSprite = CreateSprite("TownOfUs.Resources.SettingsButton.png");
            AlertSprite = CreateSprite("TownOfUs.Resources.Alert.png");
            RememberSprite = CreateSprite("TownOfUs.Resources.Remember.png");
            TrackSprite = CreateSprite("TownOfUs.Resources.Track.png");
            PoisonSprite = CreateSprite("TownOfUs.Resources.Poison.png");
            PoisonedSprite = CreateSprite("TownOfUs.Resources.Poisoned.png");
            TransportSprite = CreateSprite("TownOfUs.Resources.Transport.png");
            LighterSprite = CreateSprite("TownOfUs.Resources.Lighter.png");
            DarkerSprite = CreateSprite("TownOfUs.Resources.Darker.png");
            ToUBanner = CreateSprite("TownOfUs.Resources.TownOfUsH.png");
            activatedSensei = false;
            updatedSenseiMinimap = false;
            updatedSenseiAdminmap = false;

            PalettePatch.Load();
            ClassInjector.RegisterTypeInIl2Cpp<RainbowBehaviour>();

            // RegisterInIl2CppAttribute.Register();

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
            DirtyPatches.Initialize(_harmony);
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
    }
    public static class CustomMain
    {
        public static CustomAssets customAssets = new CustomAssets();
    }

    public class CustomAssets
    {
        // Custom Map
        public GameObject customMap;
        public GameObject customMinimap;
        public GameObject customComms;

        // Custom Lobby
        public GameObject customLobby;
    }
}
