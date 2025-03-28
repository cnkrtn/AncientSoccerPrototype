using System.Threading.Tasks;
using Core.GridService.Data;
using Core.GridService.Interface;
using UnityEngine;

namespace Core.GridService.Service
{
    public class GridService : IGridService
    {
        private Grid<GridCell> _grid;

        private const int GridWidth = 20;
        private const int GridHeight = 20;
        private const float CellSize = 10f;

        public Task Inject()
        {
            // Setup if needed later (e.g., load configs)
            return Task.CompletedTask;
        }
        
        public void InitializeGrid(Vector3 origin)
        {
            _grid = new Grid<GridCell>(
                GridWidth,
                GridHeight,
                CellSize,
                origin,
                (grid, x, y) => new GridCell(grid, x, y)
            );
        }

        public void ClearGrid()
        {
            _grid = null;
            // Optionally: inform visual layer to hide visuals
        }

        public Grid<GridCell> GetGrid() => _grid;
    }

}