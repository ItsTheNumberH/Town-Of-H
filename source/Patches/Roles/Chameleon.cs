using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TownOfUs.Roles
{
    public class Chameleon : Role
    {
        public KillButtonManager _swoopButton;
        public bool Enabled;
        public DateTime LastSwooped;
        public float TimeRemaining;

        public Chameleon(PlayerControl player) : base(player)
        {
            Name = "Chameleon";
            ImpostorText = () => "Blend into the background";
            TaskText = () => "Blend in and snoop on the impostors";
            Color = new Color(0.4f, 0.9f, 0.4f, 1f);
            RoleType = RoleEnum.Chameleon;
        }

        public bool IsSwooped => TimeRemaining > 0f;

        public KillButtonManager SwoopButton
        {
            get => _swoopButton;
            set
            {
                _swoopButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public float SwoopTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastSwooped;
            var num = CustomGameOptions.ChameleonCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public void Swoop()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            
            var color = Color.clear;
            Player.MyRend.color = color;

            Player.HatRenderer.SetHat(0, 0);
            Player.nameText.text = "";
            if (Player.MyPhysics.Skin.skin.ProdId != DestroyableSingleton<HatManager>.Instance
                .AllSkins.ToArray()[0].ProdId)
                Player.MyPhysics.SetSkin(0);
            if (Player.CurrentPet != null) Object.Destroy(Player.CurrentPet.gameObject);
            Player.CurrentPet =
                Object.Instantiate(
                    DestroyableSingleton<HatManager>.Instance.AllPets.ToArray()[0]);
            Player.CurrentPet.transform.position = Player.transform.position;
            Player.CurrentPet.Source = Player;
            Player.CurrentPet.Visible = Player.Visible;
        }


        public void UnSwoop()
        {
            Enabled = false;
            LastSwooped = DateTime.UtcNow;
            Utils.Unmorph(Player);
            Player.MyRend.color = Color.white;
        }
    }
}