using System.Collections.Generic;
using Interfaces;
using DataLoader;
using SaveData;
using Stages.Map;
using UnityEngine;

namespace InGame.Managers
{
    public class StageDataLoader: DataLoader<StageDatabaseSO>
    {
        public DreamSpawnManager noteContainer;
        public override string path => "StageInfo/Stage" + (int.Parse(GameDataContainer.currentStage.stageIndex) + 1);
        public override IDataContainer<StageDatabaseSO> dataContainer => noteContainer;
        [SerializeField] private List<AudioClip> musics;


        public override void Load()
        {
            noteContainer.music = musics[GameDataContainer.gameData.gameDataElements.currentStage];
            base.Load();
        }
    }
}