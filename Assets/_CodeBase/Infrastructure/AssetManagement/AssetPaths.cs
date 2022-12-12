namespace TankMaster._CodeBase.Infrastructure.AssetManagement
{
    public static class AssetPaths
    {
        public const string MainPlayer = "Prefabs/GamePlay/Character/Character";
        public const string Levels = "Prefabs/GamePlay/Levels";
        public const string Transition = "Prefabs/GamePlay/[TRANSITION]";
        public const string MainLight = "Prefabs/GamePlay/MainLight";
        public const string Interface = "Prefabs/UI/[INTERFACE]";
        public const string Joystick = "Prefabs/UI/Joystick";
        public const string EventSystem = "Prefabs/Infrastructure/EventSystem";
        public const string Music = "Prefabs/GamePlay/Music";
        public const string AudioService = "Prefabs/Infrastructure/AudioService";

        public static class Scenes
        {
            public const string Initial = nameof(Initial);
            public const string IntroCutscene = nameof(IntroCutscene);
            public const string Tutorial = nameof(Tutorial);
            public const string Main = nameof(Main);
        }
    }
}