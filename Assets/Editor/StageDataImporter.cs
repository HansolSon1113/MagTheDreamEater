using System.IO;
using Stages;
using Stages.Map;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class StageDataImporter : AssetPostprocessor
    {
        [MenuItem("Tools/Generate StageDatabase")]
        public static void GenerateStageDatabase()
        {
            const string folderPath = "Assets/Resources/StageInfo/";
            var csvPaths = Directory.GetFiles(folderPath, "*.csv", SearchOption.TopDirectoryOnly);

            foreach (var csvPath in csvPaths)
            {
                var csvName = Path.GetFileNameWithoutExtension(csvPath);
                var assetPath = Path.Combine(folderPath, csvName + ".asset");
                
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

                    if (float.TryParse(cols[0].Trim(), System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out var t) &&
                        float.TryParse(cols[1].Trim(), System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out var d) &&
                        float.TryParse(cols[2].Trim(), System.Globalization.NumberStyles.Float,
                            System.Globalization.CultureInfo.InvariantCulture, out var ty))
                    {
                        db.entries.Add(new StageEntry
                        {
                            startTime = t,
                            endTime = d,
                            type = ty
                        });
                    }
                    else
                    {
                        Debug.LogWarning($"[Generate] Failed to Parse: \"{lines[i]}\"");
                    }
                }


                EditorUtility.SetDirty(db);
                AssetDatabase.SaveAssets();
            }

            Debug.Log($"[Generate] Finished!");
        }
    }
}