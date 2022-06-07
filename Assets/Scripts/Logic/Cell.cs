using UnityEngine.Tilemaps;

namespace Logic
{
    public struct Cell
    {
        public const int CenterCellNeighbours = 8;
        public const int BorderCellNeighbours = 5;
        public const int CornerCellNeighbours = 3;
        public static Tile AliveTile;
        public static Tile DeadTile;

        public Tile CellTile => IsAlive ? AliveTile : DeadTile;

        public bool IsAlive { get; set; }

        private Cell[] _neighbours;

        public Cell(bool isAlive)
        {
            IsAlive = isAlive;
            _neighbours = null;
        }

        public void SetNeighbours(Cell[] neighbours)
        {
            _neighbours = neighbours;
        }

        public void Update()
        {
            int aliveNeighbours = 0;
            for (int i = 0; i < _neighbours.Length; i++)
            {
                if (_neighbours[i].IsAlive)
                    aliveNeighbours += 1;
            }

            if (IsAlive && (aliveNeighbours == 2 || aliveNeighbours == 3))
                return;

            if (IsAlive && aliveNeighbours <= 1)
            {
                IsAlive = false;
                return;
            }

            if (IsAlive && aliveNeighbours >= 4)
            {
                IsAlive = false;
                return;
            }

            if (IsAlive == false && aliveNeighbours == 3)
            {
                IsAlive = true;
                return;
            }
        }
    }
}
