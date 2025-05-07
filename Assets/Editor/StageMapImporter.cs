using System.IO;
using Stages;
using Stages.Map;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class StageMapImporter : AssetPostprocessor
    {
        [MenuItem("Tools/Generate StageMap")]
        public static void GenerateStageMap()
        {
            const string csvPath = "Assets/Resources/StageMap/Map.csv";
            const string assetPath = "Assets/Resources/StageMap/Map.asset";

            if (!File.Exists(csvPath))
            {
                Debug.LogError($"[Generate] Cannot Find CSV File: {csvPath}");
                return;
            }

            var csvText = File.ReadAllText(csvPath);
            var lines = csvText.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
            {
                Debug.LogWarning("[Generate] Data Error.");
                return;
            }

            var db = AssetDatabase.LoadAssetAtPath<StagesMapSO>(assetPath);
            if (db == null)
            {
                db = ScriptableObject.CreateInstance<StagesMapSO>();
                AssetDatabase.CreateAsset(db, assetPath);
            }

            db.nodes.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                var cols = lines[i].Split(',');
                if (cols.Length < 7)
                    continue;

                if (!string.IsNullOrEmpty(cols[0].Trim()) &&
                    !string.IsNullOrEmpty(cols[1].Trim()) &&
                    !string.IsNullOrEmpty(cols[2].Trim()) &&
                    !string.IsNullOrEmpty(cols[3].Trim()) &&
                    !string.IsNullOrEmpty(cols[4].Trim()) &&
                    !string.IsNullOrEmpty(cols[5].Trim()) &&
                    !string.IsNullOrEmpty(cols[6].Trim()))
                {
                    db.nodes.Add(new StageNode
                    {
                        stageIndex = cols[0].Trim(),
                        stageName = cols[1].Trim(),
                        stageDescription = cols[2].Trim(),
                        left = cols[3].Trim(),
                        right = cols[4].Trim(),
                        up = cols[5].Trim(),
                        down = cols[6].Trim()
                    });
                }
                else
                {
                    Debug.LogWarning($"[StageDataImporter] Failed to Parse: \"{lines[i]}\"");
                }
            }

            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
            Debug.Log($"[Generate] {db.nodes.Count} entries generated into {assetPath}");
        }
    }
}