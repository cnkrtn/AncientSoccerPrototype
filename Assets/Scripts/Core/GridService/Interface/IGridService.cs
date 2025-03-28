using System.Threading.Tasks;
using Core.GridService.Data;
using UnityEngine;

namespace Core.GridService.Interface
{
    public interface IGridService
    {
        public Task Inject();
        void InitializeGrid(Vector3 origin);
        void ClearGrid();
        Grid<GridCell> GetGrid();
    }

}