using Interfaces;
using DataLoader;
using SaveData;
using Stages;
using Stages.Map;

namespace InGame.Managers
{
    public class StageDataLoader: DataLoader<StageDatabaseSO>
    {
        public DreamSpawnManager noteContainer;
        public override string path => "StageInfo/Stage" + GameDataContainer.currentStage.stageIndex;
        public override IDataContainer<StageDatabaseSO> dataContainer => noteContainer;
    }
}