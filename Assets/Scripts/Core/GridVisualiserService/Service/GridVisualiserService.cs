using System.Collections.Generic;
using System.Threading.Tasks;
using Core.AddressableService.Interface;
using Core.GridService.Data;
using Core.GridService.Interface;
using Core.GridVisualiser.Keys;
using Core.GridVisualiserService.Interface;
using UnityEngine;

namespace Core.GridVisualiserService.Service
{
    public class GridVisualizerService : IGridVisualiserService
    {
        private readonly IGridService _gridService;
        private readonly IAddressableService _addressableService;

        private List<GameObject> _tileVisuals = new();
        private Transform _parent;

        

        public GridVisualizerService(IGridService gridService, IAddressableService addressableService)
        {
            _gridService = gridService;
            _addressableService = addressableService;
        }

        public async Task Inject()
        {
            _parent = new GameObject("GridVisuals").transform;
        }

        public async Task ShowGrid()
        {
            var grid = _gridService.GetGrid();
            if (grid == null)
            {
                Debug.LogWarning("Grid is null in GridVisualizerService.");
                return;
            }

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Vector3 worldPos = grid.GetWorldPosition(x, y);

                    GameObject tile = await _addressableService.LoadObject(GridVisualKeys.KEY_TILE_VISUAL);
                    tile.transform.position = grid.GetWorldPosition(x, y) 
                                              + new Vector3(10, 0, 10) * 0.5f 
                                              + Vector3.up * 0.01f;

                    tile.transform.SetParent(_parent);

                    tile.transform.localScale = Vector3.one * 10;
                    _tileVisuals.Add(tile);
                  

                }
            }
        }

        public void HideGrid()
        {
            foreach (var tile in _tileVisuals)
            {
                Object.Destroy(tile);
            }
            _tileVisuals.Clear();
        }
    }
}