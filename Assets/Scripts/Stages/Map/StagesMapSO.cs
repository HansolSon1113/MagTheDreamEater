using System.Collections.Generic;
using UnityEngine;

namespace Stages.Map
{
    [System.Serializable]
    public struct StageNode
    {
        public string stageIndex, stageName, stageDescription, left, right, up, down;
    }
    
    [CreateAssetMenu(fileName = "StageMap", menuName = "ScriptableObjects/StageMap", order = 1)]
    public class StagesMapSO: ScriptableObject
    {
        public List<StageNode> nodes = new List<StageNode>();
    }
}