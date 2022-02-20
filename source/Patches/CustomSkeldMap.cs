using HarmonyLib;
using System;
using static TownOfUs.TownOfUs;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TownOfUs.Patches
{
    [HarmonyPatch]
    class IntroPatch
    {
        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.SetUpRoleText))]
        class SetUpRoleTextPatch
        {
            public static void Postfix(IntroCutscene __instance) {
                // Activate sensei map
                if (CustomGameOptions.BetterSkeld) {
                    activateSenseiMap();
                }
            }
        }
        public static void activateSenseiMap() {

            // Activate custom map only on Skeld if the opcion is activated
            if (activatedSensei == false && PlayerControl.GameOptions.MapId == 0) {

                // Spawn map + assign shadow and materials layers
                GameObject senseiMap = GameObject.Instantiate(CustomMain.customAssets.customMap, PlayerControl.LocalPlayer.transform.parent);
                senseiMap.name = "HalconUI";
                senseiMap.transform.position = new Vector3(-1.5f, -1.4f, 15.05f);
                senseiMap.transform.GetChild(0).gameObject.layer = 9; // Ship Layer for HalconColisions
                senseiMap.transform.GetChild(0).transform.GetChild(0).gameObject.layer = 11; // Object Layer for HalconShadows
                senseiMap.transform.GetChild(0).transform.GetChild(1).gameObject.layer = 9; // Ship Layer for HalconAboveItems
                Material shadowShader = null;
                GameObject background = GameObject.Find("SkeldShip(Clone)/AdminHallway");
                {
                    SpriteRenderer sp = background.GetComponent<SpriteRenderer>();
                    if (sp != null) {
                        shadowShader = sp.material;
                    }
                }
                {
                    SpriteRenderer sp = senseiMap.GetComponent<SpriteRenderer>();
                    if (sp != null && shadowShader != null) {
                        sp.material = shadowShader;
                        senseiMap.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<SpriteRenderer>().material = shadowShader;
                    }
                }

                // Assign colliders objets, find halconCollisions to be the main parent
                GameObject halconCollisions = senseiMap.transform.GetChild(0).transform.gameObject; 
                
                // Area colliders rebuilded for showing map names
                GameObject colliderAdmin = GameObject.Find("SkeldShip(Clone)/Admin/Room");
                colliderAdmin.transform.SetParent(halconCollisions.transform);
                colliderAdmin.name = "RoomAdmin";
                foreach (Collider2D c in colliderAdmin.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderAdmin.transform.position = new Vector3(0, 0, 0);
                Vector2[] myAdminpoints = { new Vector2(10.09f, -3.65f), new Vector2(1.96f, -3.65f), new Vector2(0.28f, -6.09f), new Vector2(3.97f, -10.45f), new Vector2(7.12f, -10.43f) };
                colliderAdmin.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myAdminpoints;

                GameObject colliderCafeteria = GameObject.Find("SkeldShip(Clone)/Cafeteria/Room");
                colliderCafeteria.transform.SetParent(halconCollisions.transform);
                colliderCafeteria.name = "RoomCafeteria";
                foreach (Collider2D c in colliderCafeteria.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderCafeteria.transform.position = new Vector3(0, 0, 0);
                Vector2[] myCafeteriapoints = { new Vector2(4f, 3.35f), new Vector2(-2f, 3.35f), new Vector2(-2f, 4f), new Vector2(-4.5f, 6f), new Vector2(-4.5f, 0.55f), new Vector2(-2.8f, 0f), new Vector2(-2.8f, -2.64f), new Vector2(4, -2.64f) };
                colliderCafeteria.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myCafeteriapoints;

                GameObject colliderCockpit = GameObject.Find("SkeldShip(Clone)/Cockpit/Room");
                colliderCockpit.transform.SetParent(halconCollisions.transform);
                colliderCockpit.name = "RoomCookpit";
                foreach (Collider2D c in colliderCockpit.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderCockpit.transform.position = new Vector3(0, 0, 0);
                Vector2[] myCockpitpoints = { new Vector2(5f, -10f), new Vector2(5f, -13f), new Vector2(8.5f, -13f), new Vector2(8.5f, -10f) };
                colliderCockpit.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myCockpitpoints;

                GameObject colliderWeapons = GameObject.Find("SkeldShip(Clone)/Weapons/Room");
                colliderWeapons.transform.SetParent(halconCollisions.transform);
                colliderWeapons.name = "RoomWeapons";
                foreach (Collider2D c in colliderWeapons.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderWeapons.transform.position = new Vector3(0, 0, 0);
                Vector2[] myWeaponspoints = { new Vector2(12.5f, 0.5f), new Vector2(8.5f, 1.35f), new Vector2(8.5f, -3.5f), new Vector2(12.5f, -3.5f) };
                colliderWeapons.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myWeaponspoints;

                GameObject colliderLifeSupport = GameObject.Find("SkeldShip(Clone)/LifeSupport/Room");
                colliderLifeSupport.transform.SetParent(halconCollisions.transform);
                colliderLifeSupport.name = "RoomLifeSupport";
                foreach (Collider2D c in colliderLifeSupport.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderLifeSupport.transform.position = new Vector3(0, 0, 0);
                Vector2[] myLifeSupportpoints = { new Vector2(-6.66f, 1.8f), new Vector2(-8.56f, 0.75f), new Vector2(-9.1f, 0.5f), new Vector2(-9.1f, -0.6f), new Vector2(-6.3f, -0.6f), new Vector2(-6.3f, 1.8f) };
                colliderLifeSupport.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myLifeSupportpoints;

                GameObject colliderShields = GameObject.Find("SkeldShip(Clone)/Shields/Room");
                colliderShields.transform.SetParent(halconCollisions.transform);
                colliderShields.name = "RoomShields";
                foreach (Collider2D c in colliderShields.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderShields.transform.position = new Vector3(0, 0, 0);
                Vector2[] myShieldspoints = { new Vector2(4.3f, 0.3f), new Vector2(4.3f, -3.1f), new Vector2(8f, -3.1f), new Vector2(8f, 0.3f) };
                colliderShields.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myShieldspoints;

                GameObject colliderElectrical = GameObject.Find("SkeldShip(Clone)/Electrical/Room");
                colliderElectrical.transform.SetParent(halconCollisions.transform);
                colliderElectrical.name = "RoomElectrical";
                foreach (Collider2D c in colliderElectrical.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderElectrical.transform.position = new Vector3(0, 0, 0);
                Vector2[] myElectricalpoints = { new Vector2(-3.9f, -9.54f), new Vector2(-3.9f, -6.69f), new Vector2(-6.7f, -6.69f), new Vector2(-6.7f, -9.54f), new Vector2(-7.3f, -9.54f), new Vector2(-7.3f, -12.9f), new Vector2(-3.39f, -12.9f), new Vector2(-3.39f, -9.54f) };
                colliderElectrical.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myElectricalpoints;


                GameObject colliderReactor = GameObject.Find("SkeldShip(Clone)/Reactor/Room");
                colliderReactor.transform.SetParent(halconCollisions.transform);
                colliderReactor.name = "RoomReactor";
                foreach (Collider2D c in colliderReactor.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderReactor.transform.position = new Vector3(0, 0, 0);
                Vector2[] myReactorpoints = { new Vector2(-21, 2f), new Vector2(-21.5f, 0f), new Vector2(-21f, -4.2f), new Vector2(-12.6f, -2.79f), new Vector2(-12.85f, -1.25f), new Vector2(-12.6f, -0.1f) };
                colliderReactor.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myReactorpoints;

                GameObject colliderStorage = GameObject.Find("SkeldShip(Clone)/Storage/Room");
                colliderStorage.transform.SetParent(halconCollisions.transform);
                colliderStorage.name = "RoomStorage";
                foreach (Collider2D c in colliderStorage.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderStorage.transform.position = new Vector3(0, 0, 0);
                Vector2[] myStoragepoints = { new Vector2(-11.2f, -5.7f), new Vector2(-17.4f, -9f), new Vector2(-14.91f, -11.23f), new Vector2(-15.19f, -11.61f), new Vector2(-12.46f, -13.07f), new Vector2(-9.13f, -14.07f), new Vector2(-8.78f, -13.24f), new Vector2(-7.38f, -13.24f), new Vector2(-7.4f, -9.52f), new Vector2(-7.2f, -9.52f), new Vector2(-7.2f, -7.2f) };
                colliderStorage.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myStoragepoints;

                GameObject colliderRightEngine = GameObject.Find("SkeldShip(Clone)/RightEngine/Room");
                colliderRightEngine.transform.SetParent(halconCollisions.transform);
                colliderRightEngine.name = "RoomRightEngine";
                foreach (Collider2D c in colliderRightEngine.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderRightEngine.transform.position = new Vector3(0, 0, 0);
                Vector2[] myRightEnginepoints = { new Vector2(-20f, -4.5f), new Vector2(-19.15f, -6.95f), new Vector2(-16.8f, -8.9f), new Vector2(-11f, -5.1f), new Vector2(-11.75f, -4.75f), new Vector2(-12.65f, -3.25f) };
                colliderRightEngine.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myRightEnginepoints;

                GameObject colliderLeftEngine = GameObject.Find("SkeldShip(Clone)/LeftEngine/Room");
                colliderLeftEngine.transform.SetParent(halconCollisions.transform);
                colliderLeftEngine.name = "RoomLeftEngine";
                foreach (Collider2D c in colliderLeftEngine.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderLeftEngine.transform.position = new Vector3(0, 0, 0);
                Vector2[] myLeftEnginepoints = { new Vector2(-16.68f, 7.17f), new Vector2(-18.86f, 4.95f), new Vector2(-20.28f, 2.03f), new Vector2(-12.84f, 0.3f), new Vector2(-11.93f, 1.85f), new Vector2(-10.87f, 2.85f) };
                colliderLeftEngine.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myLeftEnginepoints;

                GameObject colliderComms = GameObject.Find("SkeldShip(Clone)/Comms/Room");
                colliderComms.transform.SetParent(halconCollisions.transform);
                colliderComms.name = "RoomComms";
                foreach (Collider2D c in colliderComms.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderComms.transform.position = new Vector3(0, 0, 0);
                Vector2[] myCommspoints = { new Vector2(4.3f, 4.5f), new Vector2(4.3f, 0.7f), new Vector2(8f, 0.7f), new Vector2(8f, 4.5f) };
                colliderComms.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myCommspoints;

                GameObject colliderSecurity = GameObject.Find("SkeldShip(Clone)/Security/Room");
                colliderSecurity.transform.SetParent(halconCollisions.transform);
                colliderSecurity.name = "RoomSecurity";
                foreach (Collider2D c in colliderSecurity.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderSecurity.transform.position = new Vector3(0, 0, 0);
                Vector2[] mySecuritypoints = { new Vector2(-7.9f, 10.3f), new Vector2(-7.9f, 8.25f), new Vector2(-3.75f, 8.25f), new Vector2(-3.75f, 10.3f) };
                colliderSecurity.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = mySecuritypoints;

                GameObject colliderMedical = GameObject.Find("SkeldShip(Clone)/Medical/Room");
                colliderMedical.transform.SetParent(halconCollisions.transform);
                colliderMedical.name = "RoomMedical";
                foreach (Collider2D c in colliderMedical.GetComponents<Collider2D>()) {
                    c.enabled = false;
                }
                colliderMedical.transform.position = new Vector3(0, 0, 0);
                Vector2[] myMedicalpoints = { new Vector2(-4.8f, 1.3f), new Vector2(-5.99f, 1.3f), new Vector2(-5.99f, -1.75f), new Vector2(-8.31f, -2.5f), new Vector2(-7.5f, -2.5f), new Vector2(-7.5f, -3.9f), new Vector2(-3.23f, -3.9f), new Vector2(-3.23f, -1.8f), new Vector2(-3.23f, -0.18f), new Vector2(-4.8f, -0.18f) };
                colliderMedical.transform.GetChild(0).GetComponent<PolygonCollider2D>().points = myMedicalpoints;

                // HullItems objects
                GameObject halconHullItems = senseiMap.transform.GetChild(1).transform.gameObject; // find halconHullItems to the parent
                GameObject skeldhatch0001 = GameObject.Find("hatch0001");
                skeldhatch0001.transform.SetParent(halconHullItems.transform);
                skeldhatch0001.transform.position = new Vector3(-10.33f, -14.025f, skeldhatch0001.transform.position.z);
                GameObject skeldshieldborder_off = GameObject.Find("shieldborder_off");
                skeldshieldborder_off.transform.SetParent(halconHullItems.transform);
                skeldshieldborder_off.transform.position = new Vector3(10.85f, -6.2f, skeldshieldborder_off.transform.position.z);
                GameObject skeldthruster0001lowestone = GameObject.Find("thruster0001 (1)");
                skeldthruster0001lowestone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001lowestone.transform.position = new Vector3(-24.4f, -9.25f, skeldthruster0001lowestone.transform.position.z);
                GameObject skeldthruster0001lowerone = GameObject.Find("thruster0001 (2)");
                skeldthruster0001lowerone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001lowerone.transform.position = new Vector3(-25.75f, -6, skeldthruster0001lowerone.transform.position.z);
                GameObject skeldthruster0001upperone = GameObject.Find("thruster0001");
                skeldthruster0001upperone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001upperone.transform.position = new Vector3(-25.75f, 3.275f, skeldthruster0001upperone.transform.position.z);
                GameObject skeldthruster0001higherone = GameObject.Find("thruster0001 (3)");
                skeldthruster0001higherone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001higherone.transform.position = new Vector3(-24.4f, 5.9f, skeldthruster0001higherone.transform.position.z);
                GameObject skeldthruster0001middleone = GameObject.Find("thrusterbig0001");
                skeldthruster0001middleone.transform.SetParent(halconHullItems.transform);
                skeldthruster0001middleone.transform.position = new Vector3(-28.15f, -2, skeldthruster0001middleone.transform.position.z);
                GameObject skeldweapongun = GameObject.Find("WeaponGun");
                skeldweapongun.transform.SetParent(halconHullItems.transform);
                skeldweapongun.transform.position = new Vector3(16.5f, -1.865f, skeldweapongun.transform.position.z);
                GameObject skeldlowershield = GameObject.Find("shield_off");
                skeldlowershield.transform.SetParent(halconHullItems.transform);
                skeldlowershield.transform.position = new Vector3(10.9f, -6.65f, skeldlowershield.transform.position.z);
                GameObject skelduppershield = GameObject.Find("shield_off (1)");
                skelduppershield.transform.SetParent(halconHullItems.transform);
                skelduppershield.transform.position = new Vector3(10.8f, -5.85f, skelduppershield.transform.position.z);
                GameObject skeldstarfield = GameObject.Find("starfield");
                skeldstarfield.transform.SetParent(halconHullItems.transform);
                skeldstarfield.transform.position = new Vector3(3, -4.5f, skeldstarfield.transform.position.z);

                // Admin objects
                GameObject halconAdmin = senseiMap.transform.GetChild(2).transform.gameObject; // find halconAdmin to be the parent
                GameObject skeldAdminVent = GameObject.Find("AdminVent");
                skeldAdminVent.transform.SetParent(halconAdmin.transform);
                skeldAdminVent.transform.position = new Vector3(4.17f, -10.5f, skeldAdminVent.transform.position.z);
                GameObject skeldadmintable = GameObject.Find("admin_bridge");
                skeldadmintable.transform.SetParent(halconAdmin.transform);
                skeldadmintable.transform.position = new Vector3(5.01f, -6.675f, skeldadmintable.transform.position.z);
                GameObject skeldSwipeCardConsole = GameObject.Find("SwipeCardConsole");
                skeldSwipeCardConsole.transform.SetParent(halconAdmin.transform);
                skeldSwipeCardConsole.transform.position = new Vector3(6.07f, -6.575f, skeldSwipeCardConsole.transform.position.z);
                GameObject skeldMapRoomConsole = GameObject.Find("MapRoomConsole");
                skeldMapRoomConsole.transform.SetParent(halconAdmin.transform);
                skeldMapRoomConsole.transform.position = new Vector3(3.95f, -6.575f, skeldMapRoomConsole.transform.position.z);
                GameObject skeldLeftScreen = GameObject.Find("LeftScreen");
                skeldLeftScreen.transform.SetParent(halconAdmin.transform);
                skeldLeftScreen.transform.position = new Vector3(3.56f, -3.85f, skeldLeftScreen.transform.position.z);
                GameObject skeldRightScreen = GameObject.Find("RightScreen");
                skeldRightScreen.transform.SetParent(halconAdmin.transform);
                skeldRightScreen.transform.position = new Vector3(5.55f, -3.85f, skeldRightScreen.transform.position.z);
                GameObject skeldAdminUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Admin/Ground/admin_walls/UploadDataConsole");
                skeldAdminUploadDataConsole.transform.SetParent(halconAdmin.transform);
                skeldAdminUploadDataConsole.transform.position = new Vector3(8.975f, -3.86f, skeldAdminUploadDataConsole.transform.position.z);
                GameObject skeldAdminNoOxyConsole = GameObject.Find("SkeldShip(Clone)/Admin/Ground/admin_walls/NoOxyConsole");
                skeldAdminNoOxyConsole.transform.SetParent(halconAdmin.transform);
                skeldAdminNoOxyConsole.transform.position = new Vector3(2.65f, -4f, skeldAdminNoOxyConsole.transform.position.z);
                GameObject skeldAdminFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Admin/Ground/admin_walls/FixWiringConsole");
                skeldAdminFixWiringConsole.transform.SetParent(halconAdmin.transform);
                skeldAdminFixWiringConsole.transform.position = new Vector3(6.47f, -3.87f, skeldAdminFixWiringConsole.transform.position.z);
                GameObject skeldmapComsChairs = GameObject.Find("map_ComsChairs");
                skeldmapComsChairs.transform.SetParent(halconAdmin.transform);
                skeldmapComsChairs.transform.position = new Vector3(4.585f, -4.38f, skeldmapComsChairs.transform.position.z);
                skeldadmintable.transform.GetChild(0).gameObject.SetActive(false); // Deactivate map animation

                // Cafeteria objects
                GameObject halconCafeteria = senseiMap.transform.GetChild(3).transform.gameObject; // find halconCafeteria to be the parent
                GameObject skeldCafeVent = GameObject.Find("CafeVent");
                skeldCafeVent.transform.SetParent(halconCafeteria.transform);
                skeldCafeVent.transform.position = new Vector3(-4.7f, 4, skeldCafeVent.transform.position.z);
                GameObject skeldCafeGarbageConsole = GameObject.Find("SkeldShip(Clone)/Cafeteria/Ground/GarbageConsole");
                skeldCafeGarbageConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeGarbageConsole.transform.position = new Vector3(4.69f, 4, skeldCafeGarbageConsole.transform.position.z);
                GameObject skeldCafeFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Cafeteria/Ground/FixWiringConsole");
                skeldCafeFixWiringConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeFixWiringConsole.transform.position = new Vector3(-4.15f, 2.62f, skeldCafeFixWiringConsole.transform.position.z);
                GameObject skeldCafeDataConsole = GameObject.Find("SkeldShip(Clone)/Cafeteria/Ground/DataConsole");
                skeldCafeDataConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeDataConsole.transform.position = new Vector3(-3.75f, 6.05f, skeldCafeDataConsole.transform.position.z);
                GameObject skeldCafeEmergencyConsole = GameObject.Find("EmergencyConsole");
                skeldCafeEmergencyConsole.transform.SetParent(halconCafeteria.transform);
                skeldCafeEmergencyConsole.transform.position = new Vector3(-0.65f, 1, skeldCafeEmergencyConsole.transform.position.z);

                // nav objects
                GameObject halconCockpit = senseiMap.transform.GetChild(4).transform.gameObject; // find halconCockpit to be the parent
                GameObject skeldNavVentNorth = GameObject.Find("NavVentNorth");
                skeldNavVentNorth.transform.SetParent(halconCockpit.transform);
                skeldNavVentNorth.transform.position = new Vector3(6.5f, -13.15f, skeldNavVentNorth.transform.position.z);
                GameObject skeldNavVentSouth = GameObject.Find("NavVentSouth");
                skeldNavVentSouth.transform.SetParent(halconCockpit.transform);
                skeldNavVentSouth.transform.position = new Vector3(6.5f, -15.05f, skeldNavVentSouth.transform.position.z);
                GameObject skeldNavDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Cockpit/DivertPowerConsole");
                skeldNavDivertPowerConsole.transform.SetParent(halconCockpit.transform);
                skeldNavDivertPowerConsole.transform.position = new Vector3(6.07f, -12.55f, skeldNavDivertPowerConsole.transform.position.z);
                GameObject skeldNavStabilizeSteeringConsole = GameObject.Find("StabilizeSteeringConsole");
                skeldNavStabilizeSteeringConsole.transform.SetParent(halconCockpit.transform);
                skeldNavStabilizeSteeringConsole.transform.position = new Vector3(9.21f, -14.17f, skeldNavStabilizeSteeringConsole.transform.position.z);
                GameObject skeldNavChartCourseConsole = GameObject.Find("ChartCourseConsole");
                skeldNavChartCourseConsole.transform.SetParent(halconCockpit.transform);
                skeldNavChartCourseConsole.transform.position = new Vector3(8.01f, -13.1f, skeldNavChartCourseConsole.transform.position.z);
                GameObject skeldNavUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Cockpit/Ground/UploadDataConsole");
                skeldNavUploadDataConsole.transform.SetParent(halconCockpit.transform);
                skeldNavUploadDataConsole.transform.position = new Vector3(6.59f, -12.55f, skeldNavUploadDataConsole.transform.position.z);
                GameObject skeldNavnav_chairmid = GameObject.Find("nav_chairmid");
                skeldNavnav_chairmid.transform.SetParent(halconCockpit.transform);
                skeldNavnav_chairmid.transform.position = new Vector3(8.5f, -14.1f, skeldNavnav_chairmid.transform.position.z);
                GameObject skeldNavnav_chairback = GameObject.Find("nav_chairback");
                skeldNavnav_chairback.transform.SetParent(halconCockpit.transform);
                skeldNavnav_chairback.transform.position = new Vector3(7.7f, -13.4f, skeldNavnav_chairback.transform.position.z);

                // Weapons objects
                GameObject halconWeapons = senseiMap.transform.GetChild(5).transform.gameObject; // find halconWeapons to be the parent
                GameObject skeldWeaponsVent = GameObject.Find("WeaponsVent");
                skeldWeaponsVent.transform.SetParent(halconWeapons.transform);
                skeldWeaponsVent.transform.position = new Vector3(12.25f, -2.85f, skeldWeaponsVent.transform.position.z);
                GameObject skeldWeaponsUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Weapons/Ground/UploadDataConsole");
                skeldWeaponsUploadDataConsole.transform.SetParent(halconWeapons.transform);
                skeldWeaponsUploadDataConsole.transform.position = new Vector3(11.33f, 0.3f, skeldWeaponsUploadDataConsole.transform.position.z);
                GameObject skeldWeaponsDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Weapons/Ground/weap_wall/DivertPowerConsole");
                skeldWeaponsDivertPowerConsole.transform.SetParent(halconWeapons.transform);
                skeldWeaponsDivertPowerConsole.transform.position = new Vector3(14.24f, 0.075f, skeldWeaponsDivertPowerConsole.transform.position.z);
                GameObject skeldWeaponsHeadAnim = GameObject.Find("bullettop-capglo0001");
                skeldWeaponsHeadAnim.transform.SetParent(halconWeapons.transform);
                skeldWeaponsHeadAnim.transform.position = new Vector3(10.14f, 0.525f, skeldWeaponsHeadAnim.transform.position.z);
                GameObject skeldWeaponsConsole = GameObject.Find("WeaponConsole");
                skeldWeaponsConsole.transform.SetParent(halconWeapons.transform);
                skeldWeaponsConsole.transform.position = new Vector3(11.84f, -1.25f, skeldWeaponsConsole.transform.position.z);

                // LifeSupport objects
                GameObject halconLifeSupport = senseiMap.transform.GetChild(6).transform.gameObject; // find halconLifeSupport to be the parent
                GameObject skeldLifeSupportGarbageConsole = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/GarbageConsole");
                skeldLifeSupportGarbageConsole.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportGarbageConsole.transform.position = new Vector3(-10.665f, 0.37f, skeldLifeSupportGarbageConsole.transform.position.z);
                GameObject skeldLifeSupportDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/DivertPowerConsole");
                skeldLifeSupportDivertPowerConsole.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportDivertPowerConsole.transform.position = new Vector3(-7.808f, 2.07f, skeldLifeSupportDivertPowerConsole.transform.position.z);
                GameObject skeldLifeSupportCleanFilterConsole = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/CleanFilterConsole");
                skeldLifeSupportCleanFilterConsole.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportCleanFilterConsole.transform.position = new Vector3(-9.8f, 0.82f, skeldLifeSupportCleanFilterConsole.transform.position.z);
                GameObject skeldLifeSupportLifeSuppTank = GameObject.Find("SkeldShip(Clone)/LifeSupport/Ground/LifeSuppTank");
                skeldLifeSupportLifeSuppTank.transform.SetParent(halconLifeSupport.transform);
                skeldLifeSupportLifeSuppTank.transform.position = new Vector3(-8.45f, 0.6f, skeldLifeSupportLifeSuppTank.transform.position.z);
                GameObject skeldBigYVent = GameObject.Find("BigYVent");
                skeldBigYVent.transform.SetParent(halconLifeSupport.transform);
                skeldBigYVent.transform.position = new Vector3(-9.65f, -0.4f, skeldBigYVent.transform.position.z);

                // Shields objects
                GameObject halconShields = senseiMap.transform.GetChild(7).transform.gameObject; // find halconShields to be the parent
                GameObject skeldShieldsVent = GameObject.Find("ShieldsVent");
                skeldShieldsVent.transform.SetParent(halconShields.transform);
                skeldShieldsVent.transform.position = new Vector3(5.575f, -1f, skeldShieldsVent.transform.position.z);
                GameObject skeldShieldsDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Shields/Ground/shields_floor/shields_wallside/DivertPowerConsole");
                skeldShieldsDivertPowerConsole.transform.SetParent(halconShields.transform);
                skeldShieldsDivertPowerConsole.transform.position = new Vector3(8.962f, 0.7f, skeldShieldsDivertPowerConsole.transform.position.z);
                GameObject skeldShieldLowerLeft = GameObject.Find("ShieldLowerLeft");
                skeldShieldLowerLeft.transform.SetParent(halconShields.transform);
                skeldShieldLowerLeft.transform.position = new Vector3(5.99f, -2.98f, skeldShieldLowerLeft.transform.position.z);
                GameObject skeldShieldsbulb = GameObject.Find("bulb");
                skeldShieldsbulb.transform.SetParent(halconShields.transform);
                skeldShieldsbulb.transform.position = new Vector3(9.55f, -1.05f, skeldShieldsbulb.transform.position.z);
                GameObject skeldShieldsbulbone = GameObject.Find("bulb (1)");
                skeldShieldsbulbone.transform.SetParent(halconShields.transform);
                skeldShieldsbulbone.transform.position = new Vector3(9.55f, -0.7f, skeldShieldsbulbone.transform.position.z);
                GameObject skeldShieldsbulbtwo = GameObject.Find("bulb (2)");
                skeldShieldsbulbtwo.transform.SetParent(halconShields.transform);
                skeldShieldsbulbtwo.transform.position = new Vector3(9.55f, -0.35f, skeldShieldsbulbtwo.transform.position.z);
                GameObject skeldShieldsbulbthree = GameObject.Find("bulb (3)");
                skeldShieldsbulbthree.transform.SetParent(halconShields.transform);
                skeldShieldsbulbthree.transform.position = new Vector3(5.45f, 0.15f, skeldShieldsbulbthree.transform.position.z);
                GameObject skeldShieldsbulbfour = GameObject.Find("bulb (4)");
                skeldShieldsbulbfour.transform.SetParent(halconShields.transform);
                skeldShieldsbulbfour.transform.position = new Vector3(5.75f, 0.3f, skeldShieldsbulbfour.transform.position.z);
                GameObject skeldShieldsbulbfive = GameObject.Find("bulb (5)");
                skeldShieldsbulbfive.transform.SetParent(halconShields.transform);
                skeldShieldsbulbfive.transform.position = new Vector3(6.05f, 0.45f, skeldShieldsbulbfive.transform.position.z);
                GameObject skeldShieldsbulbsix = GameObject.Find("bulb (6)");
                skeldShieldsbulbsix.transform.SetParent(halconShields.transform);
                skeldShieldsbulbsix.transform.position = new Vector3(6.35f, 0.6f, skeldShieldsbulbsix.transform.position.z);

                // Hallway objects
                GameObject halconHallway = senseiMap.transform.GetChild(8).transform.gameObject; // find halconBigHallway to be the parent
                GameObject skeldCrossHallwayFixWiringConsole = GameObject.Find("SkeldShip(Clone)/CrossHallway/FixWiringConsole");
                skeldCrossHallwayFixWiringConsole.transform.SetParent(halconHallway.transform);
                skeldCrossHallwayFixWiringConsole.transform.position = new Vector3(-8.9F, 4.93F, skeldCrossHallwayFixWiringConsole.transform.position.z);
                GameObject skeldBigYHallwayFixWiringConsole = GameObject.Find("SkeldShip(Clone)/BigYHallway/FixWiringConsole");
                skeldBigYHallwayFixWiringConsole.transform.SetParent(halconHallway.transform);
                skeldBigYHallwayFixWiringConsole.transform.position = new Vector3(4.685f, -12.53f, skeldBigYHallwayFixWiringConsole.transform.position.z);
                GameObject skeldAdminSurvCamera = GameObject.Find("SkeldShip(Clone)/AdminHallway/SurvCamera");
                skeldAdminSurvCamera.transform.SetParent(halconHallway.transform);
                skeldAdminSurvCamera.transform.position = new Vector3(5.345f, -12.45f, skeldAdminSurvCamera.transform.position.z);
                GameObject skeldBigHallwaySurvCamera = GameObject.Find("SkeldShip(Clone)/BigYHallway/SurvCamera");
                skeldBigHallwaySurvCamera.transform.SetParent(halconHallway.transform);
                skeldBigHallwaySurvCamera.transform.position = new Vector3(9.33f, 0.8f, skeldBigHallwaySurvCamera.transform.position.z);
                GameObject skeldNorthHallwaySurvCamera = GameObject.Find("SkeldShip(Clone)/NorthHallway/SurvCamera");
                skeldNorthHallwaySurvCamera.transform.SetParent(halconHallway.transform);
                skeldNorthHallwaySurvCamera.transform.position = new Vector3(-14.53f, -4.5f, skeldNorthHallwaySurvCamera.transform.position.z);
                GameObject skeldCrossHallwaySurvCamera = GameObject.Find("SkeldShip(Clone)/CrossHallway/SurvCamera");
                skeldCrossHallwaySurvCamera.transform.SetParent(halconHallway.transform);
                skeldCrossHallwaySurvCamera.transform.position = new Vector3(-9.85f, 4.75f, skeldCrossHallwaySurvCamera.transform.position.z);

                // Electrical objects
                GameObject halconElectrical = senseiMap.transform.GetChild(9).transform.gameObject; // find halconElectrical to be the parent
                GameObject skeldElecVent = GameObject.Find("ElecVent");
                skeldElecVent.transform.SetParent(halconElectrical.transform);
                skeldElecVent.transform.position = new Vector3(-5.22f, -13.95f, skeldElecVent.transform.position.z);
                GameObject skeldElecCalibrateConsole = GameObject.Find("CalibrateConsole");
                skeldElecCalibrateConsole.transform.SetParent(halconElectrical.transform);
                skeldElecCalibrateConsole.transform.position = new Vector3(-5.48f, -11.55f, skeldElecCalibrateConsole.transform.position.z);
                GameObject skeldelectric_frontset = GameObject.Find("electric_frontset");
                skeldelectric_frontset.transform.SetParent(halconElectrical.transform);
                skeldelectric_frontset.transform.position = new Vector3(-7.6f, -12.75f, skeldelectric_frontset.transform.position.z);
                GameObject skeldElecUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Electrical/Ground/UploadDataConsole");
                skeldElecUploadDataConsole.transform.SetParent(halconElectrical.transform);
                skeldElecUploadDataConsole.transform.position = new Vector3(-7.75f, -8.25f, skeldElecUploadDataConsole.transform.position.z);
                GameObject skeldElecFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Electrical/Ground/FixWiringConsole");
                skeldElecFixWiringConsole.transform.SetParent(halconElectrical.transform);
                skeldElecFixWiringConsole.transform.position = new Vector3(-6.37f, -8.725f, skeldElecFixWiringConsole.transform.position.z);
                GameObject skeldElectDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Electrical/Ground/DivertPowerConsole");
                skeldElectDivertPowerConsole.transform.SetParent(halconElectrical.transform);
                skeldElectDivertPowerConsole.transform.position = new Vector3(-8.55f, -11.25f, skeldElectDivertPowerConsole.transform.position.z);

                // Reactor objects
                GameObject halconReactor = senseiMap.transform.GetChild(10).transform.gameObject; // find halconReactor to be the parent
                GameObject skeldReactorVent = GameObject.Find("ReactorVent");
                skeldReactorVent.transform.SetParent(halconReactor.transform);
                skeldReactorVent.transform.position = new Vector3(-19.75f, -3.1f, skeldReactorVent.transform.position.z);
                GameObject skeldUpperReactorVent = GameObject.Find("UpperReactorVent");
                skeldUpperReactorVent.transform.SetParent(halconReactor.transform);
                skeldUpperReactorVent.transform.position = new Vector3(-19.75f, 0f, skeldUpperReactorVent.transform.position.z);
                GameObject skeldDivertPowerFalsePanel = GameObject.Find("DivertPowerFalsePanel");
                skeldDivertPowerFalsePanel.transform.SetParent(halconReactor.transform);
                skeldDivertPowerFalsePanel.transform.position = new Vector3(-18.6f, 1, skeldDivertPowerFalsePanel.transform.position.z);
                GameObject skeldreactor_toppipe = GameObject.Find("reactor_toppipe");
                skeldreactor_toppipe.transform.SetParent(halconReactor.transform);
                skeldreactor_toppipe.transform.position = new Vector3(-22.08f, 0.8f, skeldreactor_toppipe.transform.position.z);
                GameObject skeldreactor_base = GameObject.Find("reactor_base");
                skeldreactor_base.transform.SetParent(halconReactor.transform);
                skeldreactor_base.transform.position = new Vector3(-22.12f, -2.6f, skeldreactor_base.transform.position.z);
                GameObject skeldreactor_wireTop = GameObject.Find("reactor_wireTop");
                skeldreactor_wireTop.transform.SetParent(halconReactor.transform);
                skeldreactor_wireTop.transform.position = new Vector3(-21.21f, 0.175f, 6.7f);
                GameObject skeldreactor_wireBot = GameObject.Find("reactor_wireBot");
                skeldreactor_wireBot.transform.SetParent(halconReactor.transform);
                skeldreactor_wireBot.transform.position = new Vector3(-21.21f, -2.74f, 6.9f);
                skeldreactor_wireBot.transform.rotation = Quaternion.Euler(0f, 0f, 12.5f);

                // Storage objects
                GameObject halconStorage = senseiMap.transform.GetChild(11).transform.gameObject; // find halconStorage to be the parent
                GameObject skeldAirlockConsole = GameObject.Find("AirlockConsole");
                skeldAirlockConsole.transform.SetParent(halconStorage.transform);
                skeldAirlockConsole.transform.position = new Vector3(-9.725f, -12.6f, skeldAirlockConsole.transform.position.z);
                GameObject skeldstorage_Boxes = GameObject.Find("storage_Boxes");
                skeldstorage_Boxes.transform.SetParent(halconStorage.transform);
                skeldstorage_Boxes.transform.position = new Vector3(-13.55f, -10.4f, skeldstorage_Boxes.transform.position.z);
                GameObject skeldStorageFixWiringConsole = GameObject.Find("SkeldShip(Clone)/Storage/Ground/FixWiringConsole");
                skeldStorageFixWiringConsole.transform.SetParent(halconStorage.transform);
                skeldStorageFixWiringConsole.transform.position = new Vector3(-17.77f, -9.74f, skeldStorageFixWiringConsole.transform.position.z);

                // RightEngine objects
                GameObject halconRightEngine = senseiMap.transform.GetChild(12).transform.gameObject; // find halconRightEngine to be the parent
                GameObject skeldREngineVent = GameObject.Find("REngineVent");
                skeldREngineVent.transform.SetParent(halconRightEngine.transform);
                skeldREngineVent.transform.position = new Vector3(-18.9f, -8.7f, skeldREngineVent.transform.position.z);
                GameObject skeldRchain01 = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/chain01");
                skeldRchain01.transform.SetParent(halconRightEngine.transform);
                skeldRchain01.transform.position = new Vector3(-17.75f, -3.65f, skeldRchain01.transform.position.z);
                GameObject skeldRchain02 = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/chain02");
                skeldRchain02.transform.SetParent(halconRightEngine.transform);
                skeldRchain02.transform.position = new Vector3(-18.025f, -3.7f, skeldRchain02.transform.position.z);
                GameObject skeldRchain011 = GameObject.Find("chain01 (1)");
                skeldRchain011.transform.SetParent(halconRightEngine.transform);
                skeldRchain011.transform.position = new Vector3(-18.765f, -3.85f, skeldRchain011.transform.position.z);
                GameObject skeldREngineDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/DivertPowerConsole");
                skeldREngineDivertPowerConsole.transform.SetParent(halconRightEngine.transform);
                skeldREngineDivertPowerConsole.transform.position = new Vector3(-16.875f, -3.7f, skeldREngineDivertPowerConsole.transform.position.z);
                GameObject skeldREngineFuelEngineConsole = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/FuelEngineConsole");
                skeldREngineFuelEngineConsole.transform.SetParent(halconRightEngine.transform);
                skeldREngineFuelEngineConsole.transform.position = new Vector3(-19.65f, -7.12f, skeldREngineFuelEngineConsole.transform.position.z);
                GameObject skeldREngineAlignEngineConsole = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/AlignEngineConsole");
                skeldREngineAlignEngineConsole.transform.SetParent(halconRightEngine.transform);
                skeldREngineAlignEngineConsole.transform.position = new Vector3(-20.475f, -7.12f, skeldREngineAlignEngineConsole.transform.position.z);
                GameObject skeldREngineElectric = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/Electric");
                skeldREngineElectric.transform.SetParent(halconRightEngine.transform);
                skeldREngineElectric.transform.position = new Vector3(-19.2f, -5.475f, skeldREngineElectric.transform.position.z);
                GameObject skeldREngineSteam = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/Steam");
                skeldREngineSteam.transform.SetParent(halconRightEngine.transform);
                skeldREngineSteam.transform.position = new Vector3(-17.6f, -4.4f, skeldREngineSteam.transform.position.z);
                GameObject skeldREngineSteam1 = GameObject.Find("SkeldShip(Clone)/RightEngine/Ground/engineRight/Steam (1)");
                skeldREngineSteam1.transform.SetParent(halconRightEngine.transform);
                skeldREngineSteam1.transform.position = new Vector3(-17.6f, -7.4f, skeldREngineSteam1.transform.position.z);
                GameObject skeldengineRight = GameObject.Find("engineRight");
                skeldengineRight.transform.SetParent(halconRightEngine.transform);
                skeldengineRight.transform.position = new Vector3(-19.02f, -5.982f, skeldengineRight.transform.position.z);

                // LeftEngine objects
                GameObject halconLeftEngine = senseiMap.transform.GetChild(13).transform.gameObject; // find halconLeftEngine to be the parent
                GameObject skeldLEngineVent = GameObject.Find("LEngineVent");
                skeldLEngineVent.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineVent.transform.position = new Vector3(-18.92f, 5.8f, skeldLEngineVent.transform.position.z);
                GameObject skeldLchain01 = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/chain01");
                skeldLchain01.transform.SetParent(halconLeftEngine.transform);
                skeldLchain01.transform.position = new Vector3(-17.1f, 6.1f, skeldLchain01.transform.position.z);
                GameObject skeldLchain02 = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/chain02");
                skeldLchain02.transform.SetParent(halconLeftEngine.transform);
                skeldLchain02.transform.position = new Vector3(-16.9f, 5.95f, skeldLchain02.transform.position.z);
                GameObject skeldLEngineDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/DivertPowerConsole");
                skeldLEngineDivertPowerConsole.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineDivertPowerConsole.transform.position = new Vector3(-18.92f, 6.95f, skeldLEngineDivertPowerConsole.transform.position.z);
                GameObject skeldLEngineFuelEngineConsole = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/FuelEngineConsole");
                skeldLEngineFuelEngineConsole.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineFuelEngineConsole.transform.position = new Vector3(-19.65f, 2.48f, skeldLEngineFuelEngineConsole.transform.position.z);
                GameObject skeldLEngineAlignEngineConsole = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/AlignEngineConsole");
                skeldLEngineAlignEngineConsole.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineAlignEngineConsole.transform.position = new Vector3(-20.375f, 2.56f, skeldLEngineAlignEngineConsole.transform.position.z);
                GameObject skeldLEngineElectric = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/Electric");
                skeldLEngineElectric.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineElectric.transform.position = new Vector3(-19.2f, 4.15f, skeldLEngineElectric.transform.position.z);
                GameObject skeldLEngineSteam = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/Steam");
                skeldLEngineSteam.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineSteam.transform.position = new Vector3(-17.6f, 5.1f, skeldLEngineSteam.transform.position.z);
                GameObject skeldLEngineSteam1 = GameObject.Find("SkeldShip(Clone)/LeftEngine/Ground/engineLeft/Steam (1)");
                skeldLEngineSteam1.transform.SetParent(halconLeftEngine.transform);
                skeldLEngineSteam1.transform.position = new Vector3(-17.7f, 3.8f, skeldLEngineSteam1.transform.position.z);
                GameObject skeldengineLeft = GameObject.Find("engineLeft");
                skeldengineLeft.transform.SetParent(halconLeftEngine.transform);
                skeldengineLeft.transform.position = new Vector3(-19.02f, 3.63f, skeldengineLeft.transform.position.z);

                // Comms objects
                GameObject halconComms = senseiMap.transform.GetChild(14).transform.gameObject; // find halconComms to be the parent
                GameObject skeldFixCommsConsole = GameObject.Find("FixCommsConsole");
                skeldFixCommsConsole.transform.SetParent(halconComms.transform);
                skeldFixCommsConsole.transform.position = new Vector3(7.555f, 3.34f, skeldFixCommsConsole.transform.position.z);
                skeldFixCommsConsole.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customComms.GetComponent<SpriteRenderer>().sprite;
                GameObject skeldcommsDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Comms/Ground/comms_wallstuff/DivertPowerConsole");
                skeldcommsDivertPowerConsole.transform.SetParent(halconComms.transform);
                skeldcommsDivertPowerConsole.transform.position = new Vector3(6.95f, 5.775f, skeldcommsDivertPowerConsole.transform.position.z);
                GameObject skeldcommsUploadDataConsole = GameObject.Find("SkeldShip(Clone)/Comms/Ground/comms_wallstuff/UploadDataConsole");
                skeldcommsUploadDataConsole.transform.SetParent(halconComms.transform);
                skeldcommsUploadDataConsole.transform.position = new Vector3(8.85f, 1.87f, skeldcommsUploadDataConsole.transform.position.z);
                GameObject skeldtapescomms_tapes0001 = GameObject.Find("tapes-comms_tapes0001");
                skeldtapescomms_tapes0001.transform.SetParent(halconComms.transform);
                skeldtapescomms_tapes0001.transform.position = new Vector3(6.047f, 5.8f, skeldtapescomms_tapes0001.transform.position.z);

                // Security objects
                GameObject halconSecurity = senseiMap.transform.GetChild(15).transform.gameObject; // find halconSecurity to be the parent
                GameObject skeldSecurityVent = GameObject.Find("SecurityVent");
                skeldSecurityVent.transform.SetParent(halconSecurity.transform);
                skeldSecurityVent.transform.position = new Vector3(-8.25f, 10.7f, skeldSecurityVent.transform.position.z);
                GameObject skeldmap_surveillance = GameObject.Find("map_surveillance");
                skeldmap_surveillance.transform.SetParent(halconSecurity.transform);
                skeldmap_surveillance.transform.position = new Vector3(-6.8f, 12.26f, skeldmap_surveillance.transform.position.z);
                GameObject skeldServers = GameObject.Find("Servers");
                skeldServers.transform.SetParent(halconSecurity.transform);
                skeldServers.transform.position = new Vector3(-8.5f, 11.72f, skeldServers.transform.position.z);
                GameObject skeldsecurityDivertPowerConsole = GameObject.Find("SkeldShip(Clone)/Security/Ground/DivertPowerConsole");
                skeldsecurityDivertPowerConsole.transform.SetParent(halconSecurity.transform);
                skeldsecurityDivertPowerConsole.transform.position = new Vector3(-5.3f, 12.025f, skeldsecurityDivertPowerConsole.transform.position.z);

                // Medical objects
                GameObject halconMedical = senseiMap.transform.GetChild(16).transform.gameObject; // find halconMedical to be the parent
                GameObject skeldMedVent = GameObject.Find("MedVent");
                skeldMedVent.transform.SetParent(halconMedical.transform);
                skeldMedVent.transform.position = new Vector3(-4.35f, -1.8f, skeldMedVent.transform.position.z);
                GameObject skeldMedScanner = GameObject.Find("MedScanner");
                skeldMedScanner.transform.SetParent(halconMedical.transform);
                skeldMedScanner.transform.position = new Vector3(-8.4f, -2.915f, skeldMedScanner.transform.position.z);
                GameObject skeldMedBayConsole = GameObject.Find("MedBayConsole");
                skeldMedBayConsole.transform.SetParent(halconMedical.transform);
                skeldMedBayConsole.transform.position = new Vector3(-4.315f, -0.595f, skeldMedBayConsole.transform.position.z);


                // Change original skeld map parent and hide the innecesary vanilla objects (don't destroy them, the game won't work otherwise)
                GameObject skeldship = GameObject.Find("SkeldShip(Clone)");
                Transform[] allChildren = skeldship.transform.GetComponentsInChildren<Transform>(true);
                for (int i = 1; i < allChildren.Length - 1; i++) {
                    allChildren[i].gameObject.SetActive(false);
                }
                skeldship.transform.SetParent(halconCollisions.transform);
                activatedSensei = true;
            }
        }
    }
    
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    class HudManagerUpdatePatch
    {
        static void UpdateMiniMap() {

            if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen) {                                
                switch (PlayerControl.GameOptions.MapId) {
                    case 0:
                            GameObject minimapSabotageSkeld = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotageSkeld.SetActive(false);
                        if (activatedSensei && !updatedSenseiMinimap) {
                            GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                            mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                            GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                            hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                            // Map room names
                            GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                            minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // Upper engine
                            minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                            minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // security
                            minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                            minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                            minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                            minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                            minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                            minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // cooms
                            minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                            minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                            minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // electrical
                            minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                            // Map sabotage
                            GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                            minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey doors
                            minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // electrical doors
                            minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                            minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                            minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                            minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                            minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                            minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage electrical
                            minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                            minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                            updatedSenseiMinimap = true;
                        }
                        break;
                    case 1:
                        GameObject minimapSabotageMira = GameObject.Find("Main Camera/Hud/HqMap(Clone)/InfectedOverlay");
                        minimapSabotageMira.SetActive(false);
                        break;
                    case 2:
                        GameObject minimapSabotagePolus = GameObject.Find("Main Camera/Hud/PbMap(Clone)/InfectedOverlay");
                        minimapSabotagePolus.SetActive(false);
                        break;
                    case 3:
                        GameObject minimapSabotageDleks = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageDleks.SetActive(false);
                        break;
                    case 4:
                        GameObject minimapSabotageAirship = GameObject.Find("Main Camera/Hud/AirshipMap(Clone)/InfectedOverlay");
                        minimapSabotageAirship.SetActive(false);
                        break;
                }
            }
            else if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen) {
                switch (PlayerControl.GameOptions.MapId) {
                    case 0:
                        GameObject minimapSabotageSkeld = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageSkeld.SetActive(false);
                        if (activatedSensei && !updatedSenseiMinimap) {
                            GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                            mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                            GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                            hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                            // Map room names
                            GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                            minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // upper engine
                            minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                            minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // secutiry
                            minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                            minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                            minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                            minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                            minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                            minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // cooms
                            minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                            minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                            minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // elec
                            minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                            // Map sabotage
                            GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                            minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey
                            minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // elec doors
                            minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                            minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                            minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                            minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                            minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                            minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage elec
                            minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                            minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                            updatedSenseiMinimap = true;
                        }
                        break;
                    case 1:
                        GameObject minimapSabotageMira = GameObject.Find("Main Camera/Hud/HqMap(Clone)/InfectedOverlay");
                        minimapSabotageMira.SetActive(false);
                        break;
                    case 2:
                        GameObject minimapSabotagePolus = GameObject.Find("Main Camera/Hud/PbMap(Clone)/InfectedOverlay");
                        minimapSabotagePolus.SetActive(false);
                        break;
                    case 3:
                        GameObject minimapSabotageDleks = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageDleks.SetActive(false);
                        break;
                    case 4:
                        GameObject minimapSabotageAirship = GameObject.Find("Main Camera/Hud/AirshipMap(Clone)/InfectedOverlay");
                        minimapSabotageAirship.SetActive(false);
                        break;
                }
            }
            else if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen && PlayerControl.GameOptions.MapId == 0 && activatedSensei && !updatedSenseiMinimap) {
                GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                // Map room names
                GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // upper engine
                minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // security
                minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // cooms
                minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // elec
                minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                // Map sabotage
                GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey doors
                minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // Puertas electricidad
                minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage elec
                minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                updatedSenseiMinimap = true;
            }           
        }
        static void Postfix(HudManager __instance)
        {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;
            UpdateMiniMap();
        }
    }


    [HarmonyPatch]
    class AdminPanelPatch
    {
        static Dictionary<SystemTypes, List<Color>> players = new Dictionary<SystemTypes, List<Color>>();

        [HarmonyPatch(typeof(MapCountOverlay), nameof(MapCountOverlay.Update))]
        class MapCountOverlayUpdatePatch
        {
            static bool Prefix(MapCountOverlay __instance) {
                // Update new positions for sensei Map
                if (activatedSensei && PlayerControl.GameOptions.MapId == 0 && !updatedSenseiAdminmap) {
                    GameObject myAdminIcons = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/CountOverlay");
                    myAdminIcons.transform.GetChild(0).transform.position = myAdminIcons.transform.GetChild(0).transform.position + new Vector3(0, -0.2f, 0); // upper engine
                    myAdminIcons.transform.GetChild(1).transform.position = myAdminIcons.transform.GetChild(1).transform.position + new Vector3(0, 0.3f, 0); // lower engine
                    myAdminIcons.transform.GetChild(2).transform.position = myAdminIcons.transform.GetChild(2).transform.position + new Vector3(0.5f, 0, 0); // Reactor
                    myAdminIcons.transform.GetChild(3).transform.position = myAdminIcons.transform.GetChild(3).transform.position + new Vector3(1.6f, 2.3f, 0); // security
                    myAdminIcons.transform.GetChild(4).transform.position = myAdminIcons.transform.GetChild(4).transform.position + new Vector3(0.7f, -0.95f, 0); // medbey
                    myAdminIcons.transform.GetChild(5).transform.position = myAdminIcons.transform.GetChild(5).transform.position + new Vector3(0.5f, -1f, 0); // Cafeter�a
                    myAdminIcons.transform.GetChild(6).transform.position = myAdminIcons.transform.GetChild(6).transform.position + new Vector3(0.80f, -1, 0); // weapons
                    myAdminIcons.transform.GetChild(7).transform.position = myAdminIcons.transform.GetChild(7).transform.position + new Vector3(-1.5f, -2.6f, 0); // nav
                    myAdminIcons.transform.GetChild(8).transform.position = myAdminIcons.transform.GetChild(8).transform.position + new Vector3(0f, 1.5f, 0); // shields
                    myAdminIcons.transform.GetChild(9).transform.position = myAdminIcons.transform.GetChild(9).transform.position + new Vector3(0.9f, 3f, 0); // cooms
                    myAdminIcons.transform.GetChild(10).transform.position = myAdminIcons.transform.GetChild(10).transform.position + new Vector3(-1.7f, -0.3f, 0); // storage
                    myAdminIcons.transform.GetChild(11).transform.position = myAdminIcons.transform.GetChild(11).transform.position + new Vector3(0.20f, -0.5f, 0); // Admin
                    myAdminIcons.transform.GetChild(12).transform.position = myAdminIcons.transform.GetChild(12).transform.position + new Vector3(0.5f, -1.2f, 0); // elec
                    myAdminIcons.transform.GetChild(13).transform.position = myAdminIcons.transform.GetChild(13).transform.position + new Vector3(-2.9f, 0, 0); // o2
                    updatedSenseiAdminmap = true;
                }
                __instance.timer = 0f;
                players = new Dictionary<SystemTypes, List<Color>>();
                bool commsActive = false;
                foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                    if (task.TaskType == TaskTypes.FixComms) commsActive = true;


                if (!__instance.isSab && commsActive) {
                    __instance.isSab = true;
                    __instance.BackgroundColor.SetColor(Palette.DisabledGrey);
                    __instance.SabotageText.gameObject.SetActive(true);
                    return false;
                }
                if (__instance.isSab && !commsActive) {
                    __instance.isSab = false;
                    __instance.BackgroundColor.SetColor(Color.green);
                    __instance.SabotageText.gameObject.SetActive(false);
                }

                for (int i = 0; i < __instance.CountAreas.Length; i++) {
                    CounterArea counterArea = __instance.CountAreas[i];
                    List<Color> roomColors = new List<Color>();
                    players.Add(counterArea.RoomType, roomColors);

                    if (!commsActive) {
                        PlainShipRoom plainShipRoom = ShipStatus.Instance.FastRooms[counterArea.RoomType];

                        if (plainShipRoom != null && plainShipRoom.roomArea) {
                            int num = plainShipRoom.roomArea.OverlapCollider(__instance.filter, __instance.buffer);
                            int num2 = num;
                            for (int j = 0; j < num; j++) {
                                Collider2D collider2D = __instance.buffer[j];
                                if (!(collider2D.tag == "DeadBody")) {
                                    PlayerControl component = collider2D.GetComponent<PlayerControl>();
                                    if (!component || component.Data == null || component.Data.Disconnected || component.Data.IsDead) {
                                        num2--;
                                    }
                                    else if (component?.myRend?.material != null) {
                                        Color color = component.myRend.material.GetColor("_BodyColor");
                                        roomColors.Add(color);
                                    }
                                }
                                else {
                                    DeadBody component = collider2D.GetComponent<DeadBody>();
                                    if (component) {
                                        GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);
                                        if (playerInfo != null) {
                                            var color = Palette.PlayerColors[playerInfo.DefaultOutfit.ColorId];
                                            roomColors.Add(color);
                                        }
                                    }
                                }
                            }
                            counterArea.UpdateCount(num2);
                        }
                        else {
                            Debug.LogWarning("Couldn't find counter for:" + counterArea.RoomType);
                        }
                    }
                    else {
                        counterArea.UpdateCount(0);
                    }
                }
                return false;
            }
        }
    }
}