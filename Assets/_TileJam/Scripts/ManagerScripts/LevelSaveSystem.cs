using System.IO;
using _TileJam.Scripts.LevelCreatorScripts;
using Newtonsoft.Json;
using UnityEngine;

namespace _TileJam.Scripts.ManagerScripts
{
    public  static class LevelSaveSystem
    {
        private static string GetFilePath(int index)
        {
            return Path.Combine(Application.dataPath, "LevelData", $"level_{index}.json");
        }

        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        public static void SaveLevelData(LevelData levelData, int index)
        {
            string json = JsonConvert.SerializeObject(levelData, settings);
            string path = GetFilePath(index);

            Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);
            File.WriteAllText(path, json);
        }

        public static LevelData LoadLevelData(int index)
        {
            string path = GetFilePath(index);
            if (!File.Exists(path)) return null;

            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<LevelData>(json, settings);
        }
    }
}
