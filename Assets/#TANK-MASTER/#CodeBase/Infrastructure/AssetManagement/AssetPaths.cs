namespace TankMaster.Infrastructure.AssetManagement
{
    public static class AssetPaths
    {
        public const string
            MainPlayerID = "Assets/#TANK-MASTER/Res/Prefabs/GamePlay/Character/Character.prefab",
            LevelsID = "TANK-MASTER/Res/Prefabs/GamePlay/Levels",
            TransitionID = "Assets/#TANK-MASTER/Res/Prefabs/GamePlay/(TRANSITION).prefab",
            MainLightID = "Assets/#TANK-MASTER/Res/Prefabs/GamePlay/MainLight.prefab",
            InterfaceID = "Assets/#TANK-MASTER/Res/Prefabs/UI/(INTERFACE).prefab",
            JoystickID = "Assets/#TANK-MASTER/Res/Prefabs/UI/Joystick.prefab",
            EventSystemID = "Assets/#TANK-MASTER/Res/Prefabs/Infrastructure/EventSystem.prefab",
            MusicID = "Assets/#TANK-MASTER/Res/Prefabs/GamePlay/Music.prefab",
            AudioServiceID = "Assets/#TANK-MASTER/Res/Prefabs/Infrastructure/AudioService.prefab";

        public static class Scenes
        {
            public const string
                Initial = nameof(Initial),
                IntroCutscene = nameof(IntroCutscene),
                Tutorial = nameof(Tutorial), 
                Main = nameof(Main);
        }
    }
}