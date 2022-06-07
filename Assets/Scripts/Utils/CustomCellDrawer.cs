using Logic;
using UnityEngine;

namespace Utils
{
    public class CustomCellDrawer
    {
        private readonly Field _field;
        private readonly Grid _grid;

        public CustomCellDrawer(Field field, Grid grid)
        {
            _field = field;
            _grid = grid;
        }

        public void PlaceNewCell(Vector3 screenPosition)
        {
            _field.AddCell(FindCellCoordinates(screenPosition));
        }

        public void RemoveCell(Vector3 screenPosition)
        {
            _field.RemoveCell(FindCellCoordinates(screenPosition));
        }

        private Vector3Int FindCellCoordinates(Vector3 mousePosition)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector3Int cellPosition = _grid.WorldToCell(worldPoint);
            return cellPosition;
        }
    }
}