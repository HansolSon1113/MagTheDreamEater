using Interfaces;
using DataLoader;
using SaveData;
using Stages.Map;

namespace InGame.Managers
{
    public class StageDataLoader: DataLoader<StageDatabaseSO>
    {
        public DreamSpawnManager noteContainer;
        public override string path => "StageInfo/Stage" + int.Parse(GameDataContainer.currentStage.stageIndex + 1);
        public override IDataContainer<StageDatabaseSO> dataContainer => noteContainer;
    }
}