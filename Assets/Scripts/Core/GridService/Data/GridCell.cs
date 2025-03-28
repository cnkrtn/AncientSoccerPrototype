namespace Core.GridService.Data
{
    public class GridCell
    {
        public int X { get; }
        public int Y { get; }

        public bool IsWalkable { get; set; } = true;
        public bool IsOccupied { get; set; } = false;

        public GridCell(Grid<GridCell> grid, int x, int y)
        {
            X = x;
            Y = y;
        }
    }

}