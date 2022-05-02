using UnityEngine;
using UnityEngine.Tilemaps;

public class Field
{
    public Cell[,] Cells => _cells;

    private int _width;
    private int _height;
    private Tilemap _renderField;

    private Cell[,] _cells;

    public int Width {
        get => _width;
        private set
        {
            if (value <= 0)
            {
                ThrowGridSizeError("Ширина поля не может быть меньше либо равна нулю. Установка значения 1",
                    ref _width, 1);
                return;
            }
            _width = value;
        }
    }
    public int Height { 
        get => _height;
        private set
        {
            if (value <= 0)
            {
                ThrowGridSizeError("Высота поля не может быть меньше либо равна нулю. Установка значения 1",
                    ref _height, 1);
                return;
            }
            _height = value;
        }
    }

    public Field(int width, int height, Tilemap renderField)
    {
        Width = width;
        Height = height;
        _renderField = renderField;
        GenerateEmptyField();
    }

    public void Render()
    {
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                _renderField.SetTile(new Vector3Int(x, y, 0), _cells[x, y].CellTile);
            }
        }
    }

    public void AddCell(int x, int y)
    {
        _cells[x, y].IsAlive = true;
    }

    public void AddCell(Vector3Int cellPosition)
    {
        _cells[cellPosition.x, cellPosition.y].IsAlive = true;
    }

    public void RemoveCell(int x, int y)
    {
        _cells[x, y].IsAlive = false;
    }

    public void RemoveCell(Vector3Int cellPosition)
    {
        _cells[cellPosition.x, cellPosition.y].IsAlive = false;
    }

    public void SetCellNeighbours()
    {
        for (int i = 0; i < _cells.GetLength(0); i++)
        {
            for (int j = 0; j < _cells.GetLength(1); j++)
            {
                _cells[i, j].SetNeighbours(GetCellNeighbours(i, j));
            }
        }
    }

    private Cell[] GetCellNeighbours(int x, int y)
    {
        Cell[] neighbours = null;

        if (x > 0 && y > 0 && x < _width - 1 && y < _height - 1) // где-то в середине
        {
            neighbours = new Cell[Cell.CenterCellNeighbours];
            neighbours[0] = _cells[x - 1, y - 1];
            neighbours[1] = _cells[x - 1, y];
            neighbours[2] = _cells[x - 1, y + 1];
            neighbours[3] = _cells[x, y + 1];
            neighbours[4] = _cells[x + 1, y + 1];
            neighbours[5] = _cells[x + 1, y];
            neighbours[6] = _cells[x + 1, y - 1];
            neighbours[7] = _cells[x, y - 1];
        }
        else if (x == 0 && y > 0 && y < _height - 1) // левая стенка
        {
            neighbours = new Cell[Cell.BorderCellNeighbours];
            neighbours[0] = _cells[x, y - 1];
            neighbours[1] = _cells[x + 1, y - 1];
            neighbours[2] = _cells[x + 1, y];
            neighbours[3] = _cells[x + 1, y + 1];
            neighbours[4] = _cells[x, y + 1];
        }
        else if (y == 0 && x > 0 && x < _width - 1) // нижняя стенка
        {
            neighbours = new Cell[Cell.BorderCellNeighbours];
            neighbours[0] = _cells[x - 1, y];
            neighbours[1] = _cells[x - 1, y + 1];
            neighbours[2] = _cells[x, y + 1];
            neighbours[3] = _cells[x + 1, y + 1];
            neighbours[4] = _cells[x + 1, y];
        }
        else if (x == _width - 1 && y > 0 && y < _height - 1) // правая стенка
        {
            neighbours = new Cell[Cell.BorderCellNeighbours];
            neighbours[0] = _cells[x, y + 1];
            neighbours[1] = _cells[x - 1, y + 1];
            neighbours[2] = _cells[x - 1, y];
            neighbours[3] = _cells[x - 1, y - 1];
            neighbours[4] = _cells[x, y - 1];
        }
        else if (x > 0 && y == _height - 1 && x < _width - 1) // верхняя стенка
        {
            neighbours = new Cell[Cell.BorderCellNeighbours];
            neighbours[0] = _cells[x - 1, y];
            neighbours[1] = _cells[x - 1, y - 1];
            neighbours[2] = _cells[x, y - 1];
            neighbours[3] = _cells[x + 1, y - 1];
            neighbours[4] = _cells[x + 1, y];
        }
        else if (x == 0 && y == 0) // левый нижний угол
        {
            neighbours = new Cell[Cell.CornerCellNeighbours];
            neighbours[2] = _cells[x, y + 1];
            neighbours[1] = _cells[x + 1, y + 1];
            neighbours[0] = _cells[x + 1, y];
        }
        else if (x == 0 && y == _height - 1) // левый верхний угол
        {
            neighbours = new Cell[Cell.CornerCellNeighbours];
            neighbours[0] = _cells[x, y - 1];
            neighbours[1] = _cells[x + 1, y - 1];
            neighbours[2] = _cells[x + 1, y];
        }
        else if (x == _width - 1 && y == 0) // правый нижний угол
        {
            neighbours = new Cell[Cell.CornerCellNeighbours];
            neighbours[0] = _cells[x - 1, y];
            neighbours[1] = _cells[x - 1, y + 1];
            neighbours[2] = _cells[x, y + 1];
        }
        else if (x == _width - 1 && y == _height - 1) // правый верхний угол
        {
            neighbours = new Cell[Cell.CornerCellNeighbours];
            neighbours[0] = _cells[x - 1, y];
            neighbours[1] = _cells[x - 1, y - 1];
            neighbours[2] = _cells[x, y - 1];
        }

        return neighbours;
    }

    private void GenerateEmptyField()
    {
        _cells = new Cell[Width, Height];
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                _cells[x, y] = new Cell(false);
            }
        } 
    }

    private void ThrowGridSizeError(string message, ref int exceptionValue, int defaultValue)
    {
        Debug.LogError(message);
        exceptionValue = defaultValue;
    }
}
