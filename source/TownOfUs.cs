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
using TownOfUs.CustomHats;
using TownOfUs.CustomOption;
using TownOfUs.Extensions;
using TownOfUs.RainbowMod;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TownOfUs
{
    [BepInPlugin(Id, "Town Of Us", VersionString)]
    [BepInDependency(ReactorPlugin.Id)]
    public class TownOfUs : BasePlugin
    {
        public const string Id = "com.slushiegoose.townofus";
        public const string VersionString = "4.0.13";
        public static System.Version Version = System.Version.Parse(VersionString);
        
        public static Sprite LogoBanner;
        public static Sprite JanitorClean;
        public static Sprite CannibalEat;
        public static Sprite EngineerFix;
        public static Sprite SwapperSwitch;
        public static Sprite SwapperSwitchDisabled;
        public static Sprite Shift;
        public static Sprite Footprint;
        public static Sprite Rewind;
        public static Sprite NormalKill;
        public static Sprite ShiftKill;
        public static Sprite MedicSprite;
        public static Sprite TrackerSprite;
        public static Sprite SeerSprite;
        public static Sprite SampleSprite;
        public static Sprite MorphSprite;
        public static Sprite FrameSprite;
        public static Sprite PoisonSprite;
        public static Sprite PoisonedSprite;
        public static Sprite Camouflage;
        public static Sprite Arrow;
        public static Sprite Abstain;
        public static Sprite MineSprite;
        public static Sprite SwoopSprite;
        public static Sprite HideSprite;

        public static Sprite DouseSprite;
        public static Sprite IgniteSprite;
        public static Sprite ReviveSprite;
        public static Sprite ButtonSprite;
        public static Sprite CycleSprite;
        public static Sprite GuessSprite;


        public static Sprite DragSprite;
        public static Sprite DropSprite;
        public static Sprite FlashSprite;

        private static DLoadImage _iCallLoadImage;


        private Harmony _harmony;

        public ConfigEntry<string> Ip { get; set; }

        public ConfigEntry<ushort> Port { get; set; }

        public override void Load()
        {
            System.Console.WriteLine("000.000.000.000/000000000000000000");

            _harmony = new Harmony("com.slushiegoose.townofus");

            Generate.GenerateAll();

            LogoBanner = CreateSprite("TownOfUs.Resources.TownOfH.png");
            JanitorClean = CreateSprite("TownOfUs.Resources.Janitor.png");
            CannibalEat = CreateSprite("TownOfUs.Resources.Eat.png");
            EngineerFix = CreateSprite("TownOfUs.Resources.Engineer.png");
            SwapperSwitch = CreateSprite("TownOfUs.Resources.SwapperSwitch.png");
            SwapperSwitchDisabled = CreateSprite("TownOfUs.Resources.SwapperSwitchDisabled.png");
            Shift = CreateSprite("TownOfUs.Resources.Shift.png");
            Footprint = CreateSprite("TownOfUs.Resources.Footprint.png");
            Rewind = CreateSprite("TownOfUs.Resources.Rewind.png");
            NormalKill = CreateSprite("TownOfUs.Resources.NormalKill.png");
            ShiftKill = CreateSprite("TownOfUs.Resources.ShiftKill.png");
            MedicSprite = CreateSprite("TownOfUs.Resources.Medic.png");
            SeerSprite = CreateSprite("TownOfUs.Resources.Seer.png");
            SampleSprite = CreateSprite("TownOfUs.Resources.Sample.png");
            MorphSprite = CreateSprite("TownOfUs.Resources.Morph.png");
            FrameSprite = CreateSprite("TownOfUs.Resources.Frame.png");
            PoisonSprite = CreateSprite("TownOfUs.Resources.Poison.png");
            PoisonedSprite = CreateSprite("TownOfUs.Resources.Poisoned.png");
            Camouflage = CreateSprite("TownOfUs.Resources.Camouflage.png");
            Arrow = CreateSprite("TownOfUs.Resources.Arrow.png");
            Abstain = CreateSprite("TownOfUs.Resources.Abstain.png");
            MineSprite = CreateSprite("TownOfUs.Resources.Mine.png");
            SwoopSprite = CreateSprite("TownOfUs.Resources.Swoop.png");
            HideSprite = CreateSprite("TownOfUs.Resources.Hide.png");
            DouseSprite = CreateSprite("TownOfUs.Resources.Douse.png");
            IgniteSprite = CreateSprite("TownOfUs.Resources.Ignite.png");
            ReviveSprite = CreateSprite("TownOfUs.Resources.Revive.png");
            ButtonSprite = CreateSprite("TownOfUs.Resources.Button.png");
            DragSprite = CreateSprite("TownOfUs.Resources.Drag.png");
            DropSprite = CreateSprite("TownOfUs.Resources.Drop.png");
            CycleSprite = CreateSprite("TownOfUs.Resources.Cycle.png");
            GuessSprite = CreateSprite("TownOfUs.Resources.Guess.png");
            FlashSprite = CreateSprite("TownOfUs.Resources.Flash.png");
            TrackerSprite = CreateSprite("TownOfUs.Resources.Track.png");

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

            SceneManager.add_sceneLoaded((Action<Scene, LoadSceneMode>) ((scene, loadSceneMode) =>
            {
                ModManager.Instance.ShowModStamp();
            }));

            _harmony.PatchAll();
            DirtyPatches.Initialize(_harmony);
        }

        public static Sprite CreateSprite(string name, bool hat = false)
        {
            var pixelsPerUnit = hat ? 225f : 100f;
            var pivot = hat ? new Vector2(0.5f, 0.8f) : new Vector2(0.5f, 0.5f);

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

        public static Sprite CreatePolusHat(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();

            var tex = new Texture2D(128, 128, (TextureFormat) 1, false);
            LoadImage(tex, img, false);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            sprite.DontDestroy();
            return sprite;
        }

        private static void LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            _iCallLoadImage ??= IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
            var il2CPPArray = (Il2CppStructArray<byte>) data;
            _iCallLoadImage.Invoke(tex.Pointer, il2CPPArray.Pointer, markNonReadable);
        }

        private delegate bool DLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
    }
}
