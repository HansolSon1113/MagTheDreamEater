using System.IO;
using Stages;
using Stages.Map;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class StageDataImporter : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            const string csvRelativePath = "Assets/Resources/StageInfo/Test_Stage.csv";
            const string assetRelativePath = "Assets/Resources/StageInfo/Test_Stage.asset";

            foreach (var path in importedAssets)
            {
                if (path != csvRelativePath) continue;

                var csvText = File.ReadAllText(path);
                var lines = csvText
                    .Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2)
                {
                    Debug.LogWarning("[StageDataImporter] Data Error.");
                    return;
                }
            
                var db = AssetDatabase.LoadAssetAtPath<StageDatabaseSO>(assetRelativePath);
                if (db == null)
                {
                    db = ScriptableObject.CreateInstance<StageDatabaseSO>();
                    AssetDatabase.CreateAsset(db, assetRelativePath);
                }

                db.entries.Clear();
            
                for (int i = 1; i < lines.Length; i++)
                {
                    var cols = lines[i].Split(',');
                    if (cols.Length < 3) 
                        continue;

                    if (float.TryParse(cols[0].Trim(),    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var t) &&
                        float.TryParse(cols[1].Trim(),    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var d) &&
                        float.TryParse(cols[2].Trim(),    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var ty))
                    {
                        db.entries.Add(new StageEntry {
                            time     = t,
                            duration = d,
                            type     = ty
                        });
                    }
                    else
                    {
                        Debug.LogWarning($"[StageDataImporter] Failed to Parse: \"{lines[i]}\"");
                    }
                }

                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();
                Debug.Log($"[StageDataImporter] {db.entries.Count} entries imported into {assetRelativePath}");
            }
        }
        
        [MenuItem("Tools/Generate StageDatabase")]
        public static void GenerateStageDatabase()
        {
            const string csvPath   = "Assets/Resources/StageInfo/Test_Stage.csv";
            const string assetPath = "Assets/Resources/StageInfo/Test_Stage.asset";

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

            var db = AssetDatabase.LoadAssetAtPath<StageDatabaseSO>(assetPath);
            if (db == null)
            {
                db = ScriptableObject.CreateInstance<StageDatabaseSO>();
                AssetDatabase.CreateAsset(db, assetPath);
            }
            db.entries.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                var cols = lines[i].Split(',');
                if (cols.Length < 3) continue;

                if (float.TryParse(cols[0].Trim(),    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var t) &&
                    float.TryParse(cols[1].Trim(),    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var d) &&
                    float.TryParse(cols[2].Trim(),    System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var ty))
                {
                    db.entries.Add(new StageEntry {
                        time     = t,
                        duration = d,
                        type     = ty
                    });
                }
                else
                {
                    Debug.LogWarning($"[Generate] Failed to Parse: \"{lines[i]}\"");
                }
            }

            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
            Debug.Log($"[Generate] {db.entries.Count} entries generated into {assetPath}");
        }
    }
}
