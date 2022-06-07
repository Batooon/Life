using Logic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

public static class Parameters
{
    public const int RightMouseButton = 1;
    public const int LeftMouseButton = 0;
}

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private Tile _aliveCell;
    [SerializeField] private Tile _deadCell;
    [SerializeField] private Tilemap _renderField;
    [SerializeField] private int _fieldWidth;
    [SerializeField] private int _fieldHeight;
    [SerializeField] private float _iterationRepeatRate;
    [SerializeField] private float _iterationRepeatTime;

    private Field _gameField;
    private Lifecycle _lifecycle;
    private CustomCellDrawer _cellDrawer;
    private bool _simulationStarted;

    private Vector3 _mousePosition;

    private void Awake()
    {
        Cell.AliveTile = _aliveCell;
        Cell.DeadTile = _deadCell;

        _gameField = new Field(_fieldWidth, _fieldHeight, _renderField);
        _lifecycle = new Lifecycle();
        _cellDrawer = new CustomCellDrawer(_gameField, _grid);

        _gameField.SetCellNeighbours();

        _gameField.Render();
    }

    private void Update()
    {
        if (_simulationStarted) 
            return;
        
        _mousePosition = Input.mousePosition;
            
        if (Input.GetMouseButtonDown(Parameters.LeftMouseButton))
        {
            _cellDrawer.PlaceNewCell(_mousePosition);
            _gameField.Render();
            _gameField.SetCellNeighbours();
        }
        else if (Input.GetMouseButtonDown(Parameters.RightMouseButton))
        {
            _cellDrawer.RemoveCell(_mousePosition);
            _gameField.Render();
            _gameField.SetCellNeighbours();
        }
    }

    [ContextMenu("start simulation")]
    public void StartSimulation()
    {
        _simulationStarted = true;
        InvokeRepeating(nameof(Iterate), _iterationRepeatTime, _iterationRepeatRate);
    }

    private void Iterate()
    {
        _gameField.Render();
        _lifecycle.Iterate(_gameField);
        _gameField.SetCellNeighbours();
    }
}
