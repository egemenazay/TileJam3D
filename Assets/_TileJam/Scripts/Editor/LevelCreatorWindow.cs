using _TileJam.Scripts.LevelCreatorScripts.LevelData;
using _TileJam.Scripts.LevelEditorScripts.Grid;
using _TileJam.Scripts.ManagerScripts;
using UnityEditor;
using UnityEngine;

namespace _TileJam.Scripts.Editor
{
    public class LevelCreatorWindow : EditorWindow
    {
        private int levelIndex = 0;
        private int defaultWidth = 10;
        private int defaultHeight = 10;

        private LevelData levelData;

        [MenuItem("Window/Level Creator")]
        public static void ShowWindow()
        {
            GetWindow<LevelCreatorWindow>("Level Creator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Level Creator", EditorStyles.boldLabel);

            levelIndex = EditorGUILayout.IntField("Level Index", levelIndex);
            defaultWidth = EditorGUILayout.IntField("Default Width", defaultWidth);
            defaultHeight = EditorGUILayout.IntField("Default Height", defaultHeight);

            GUILayout.Space(10);

            if (GUILayout.Button("Load Level"))
            {
                levelData = LevelDataManager.LoadLevelData(levelIndex);
                if (levelData == null)
                {
                    Debug.Log($"[LevelCreator] Level {levelIndex} bulunamadı, yeni level oluşturuluyor.");
                    levelData = new LevelData();
                    levelData.Initialize(defaultWidth, defaultHeight);
                }
            }

            if (levelData != null)
            {
                if (GUILayout.Button("Save Level"))
                {
                    LevelDataManager.SaveLevelData(levelData, levelIndex);
                    Debug.Log($"[LevelCreator] Level {levelIndex} kaydedildi.");
                }

                GUILayout.Space(10);
                DrawGrid();
            }
        }

        private void DrawGrid()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.fontStyle = FontStyle.Bold;

            float totalWidth = levelData.width * 52f; // 50 button + 2 padding
            float windowCenter = position.width / 2f;
            float offsetX = windowCenter - (totalWidth / 2f);

            GUILayout.BeginHorizontal();
            GUILayout.Space(offsetX);
            GUILayout.BeginVertical();

            for (int y = levelData.height - 1; y >= 0; y--)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < levelData.width; x++)
                {
                    GridCell cell = levelData.gridCells[x, y];
                    Color originalColor = GUI.backgroundColor;
                    GUI.backgroundColor = cell.isOccupied ? Color.green : Color.gray;

                    string label = $"{x}x{y}";

                    if (GUILayout.Button(label, buttonStyle, GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        Event e = Event.current;
                        if (e.button == 1) // Right Click
                        {
                            levelData.gridCells[x, y].isOccupied = false;
                        }
                        else // Left Click or default
                        {
                            levelData.gridCells[x, y].isOccupied = true;
                        }
                        GUI.FocusControl(null); // Prevent focus lock
                    }

                    GUI.backgroundColor = originalColor;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
    }
}
