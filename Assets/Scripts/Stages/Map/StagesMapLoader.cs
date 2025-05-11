using System.Collections.Generic;
using DataLoader;
using Interfaces;
using UnityEngine;

namespace Stages.Map
{ 
    public class StagesMapLoader : DataLoader<StagesMapSO>
    {
        [SerializeField] private StagesManager mapContainer;
        public override IDataContainer<StagesMapSO> dataContainer => mapContainer;
        public override string path => "StageMap/Map";
        public List<GameObject> objects = new List<GameObject>();
        
        public override void Load()
        {
            mapContainer.stageObjects = objects;
            base.Load();
        }
    }
}