using System.Collections.Generic;
using Stages.Map;
using UnityEngine;

namespace Interfaces
{
    public interface IMapContainer: IDataContainer<StagesMapSO>
    {
        public StagesMapSO stageMap { get; set; }
        public List<GameObject> stageObjects { get; set; }
    }
}