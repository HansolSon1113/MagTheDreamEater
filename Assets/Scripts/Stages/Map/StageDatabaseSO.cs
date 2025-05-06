using System.Collections.Generic;
using UnityEngine;

namespace Stages.Map
{
    public class StageDatabaseSO : ScriptableObject
    {
        public List<StageEntry> entries = new List<StageEntry>();
    }
}