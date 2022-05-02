using UnityEngine.Tilemaps;

public struct Cell
{
    public static int CenterCellNeighbours = 8;
    public static int BorderCellNeighbours = 5;
    public static int CornerCellNeighbours = 3;
    public static Tile AliveTile;
    public static Tile DeadTile;

    public Tile CellTile
    {
        get
        {
            return _isAlive ? AliveTile : DeadTile;
        }
    }

    private bool _isAlive;

    public bool IsAlive
    {
        get => _isAlive;
        set => _isAlive = value;
    }

    private Cell[] _neighbours;

    public Cell(bool isAlive)
    {
        _isAlive = isAlive;
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

        if (_isAlive && (aliveNeighbours == 2 || aliveNeighbours == 3))
            return;

        if (_isAlive && aliveNeighbours <= 1)
        {
            _isAlive = false;
            return;
        }

        if (_isAlive && aliveNeighbours >= 4)
        {
            _isAlive = false;
            return;
        }

        if (_isAlive == false && aliveNeighbours == 3)
        {
            _isAlive = true;
            return;
        }
    }
}
