using _TileJam.Scripts.ManagerScripts;
using UnityEngine;

namespace _TileJam.Scripts.LevelCreatorScripts
{
    public class LevelCreator : MonoBehaviour
    {
    
        public int levelIndex = 0;
        public int defaultWidth = 10;
        public int defaultHeight = 10;

        [HideInInspector]
        public LevelData levelData;

        public void LoadLevel()
        {
            levelData = LevelSaveSystem.LoadLevelData(levelIndex);
            if (levelData == null)
            {
                Debug.Log($"[LevelCreator] Level {levelIndex} Not Found, Creating New Level");
                levelData = new LevelData();
                levelData.Initialize(defaultWidth, defaultHeight);
            }
        }

        public void SaveLevel()
        {
            if (levelData != null)
            {
                LevelSaveSystem.SaveLevelData(levelData, levelIndex);
                Debug.Log($"[LevelCreator] Level {levelIndex} Saved.");
            }
        }
    }
}
