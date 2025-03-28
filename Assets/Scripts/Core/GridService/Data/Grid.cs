using System;
using UnityEngine;

namespace Core.GridService.Data
{
    public class Grid<T>
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;
        private T[,] _gridArray;

        public Vector3 OriginPosition => _originPosition;
        public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createFunc)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new T[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _gridArray[x, y] = createFunc(this, x, y);
                }
            }
        }

        public Vector3 GetWorldPosition(int x, int y)
            => new Vector3(x, 0, y) * _cellSize + _originPosition;

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.RoundToInt((worldPosition - _originPosition).x / _cellSize - 0.5f);
            y = Mathf.RoundToInt((worldPosition - _originPosition).z / _cellSize - 0.5f);

        }

        public bool IsInBounds(int x, int y)
            => x >= 0 && y >= 0 && x < _width && y < _height;
        

        public T GetGridObject(int x, int y)
            => IsInBounds(x, y) ? _gridArray[x, y] : default;

        public void SetGridObject(int x, int y, T value)
        {
            if (IsInBounds(x, y))
            {
                _gridArray[x, y] = value;
            }
        }
    }

}