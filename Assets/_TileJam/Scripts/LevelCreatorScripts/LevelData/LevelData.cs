using _TileJam.Scripts.LevelEditorScripts.Grid;

namespace _TileJam.Scripts.LevelCreatorScripts.LevelData
{ 
    [System.Serializable]
    public class LevelData
    {
        public int width;
        public int height;
        public GridCell[,] gridCells;

        public void Initialize(int width, int height)
        {
            this.width = width;
            this.height = height;
            gridCells = new GridCell[width , height];
        }
    }
}
