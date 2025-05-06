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
        public List<Transform> transforms = new List<Transform>();
        
        public override void Load()
        {
            mapContainer.stageTransforms = transforms;
            base.Load();
        }
    }
}