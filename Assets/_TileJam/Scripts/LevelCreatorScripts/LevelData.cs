namespace _TileJam.Scripts.LevelCreatorScripts
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
