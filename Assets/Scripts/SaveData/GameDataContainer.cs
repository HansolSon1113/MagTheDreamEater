using Stages.Map;

namespace SaveData
{
    public static class GameDataContainer
    {
        public static GameData gameData = new();
        public static SettingData settingData = new();
        public static StageNode currentStage;
    }
}