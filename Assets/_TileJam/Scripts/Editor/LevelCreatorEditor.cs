using _TileJam.Scripts.LevelCreatorScripts;
using UnityEditor;
using UnityEngine;

namespace _TileJam.Scripts.Editor
{
    
    [CustomEditor(typeof(LevelCreator))]
    public class LevelCreatorEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        LevelCreator creator = (LevelCreator)target;

        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Load Level"))
        {
            creator.LoadLevel();
        }

        if (creator.levelData != null)
        {
            if (GUILayout.Button("Save Level"))
            {
                creator.SaveLevel();
            }

            GUILayout.Space(10);
            DrawGrid(creator);
        }
    }

    private void DrawGrid(LevelCreator creator)
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        buttonStyle.fontStyle = FontStyle.Bold;

        float totalWidth = creator.levelData.width * 52f;
        float windowCenter = EditorGUIUtility.currentViewWidth / 2f;
        float offsetX = windowCenter - (totalWidth / 2f);
        
        if (creator.levelData.gridCells == null)
        {
            EditorGUILayout.HelpBox("No Grid data. Please 'Load Level' first.", MessageType.Warning);
            return;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Space(offsetX);
        GUILayout.BeginVertical();

        for (int y = creator.levelData.height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < creator.levelData.width; x++)
            {
                GridCell cell = creator.levelData.gridCells[x, y];
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = cell.isOccupied ? Color.green : Color.gray;

                string label = $"{x}x{y}";

                if (GUILayout.Button(label, buttonStyle, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    Event e = Event.current;
                    if (e.button == 1)
                    {
                        creator.levelData.gridCells[x, y].isOccupied = false;
                    }
                    else
                    {
                        creator.levelData.gridCells[x, y].isOccupied = true;
                    }
                    GUI.FocusControl(null);
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
