namespace Logic
{
    public class Lifecycle
    {
        public void Iterate(Field field)
        {
            for (var i = 0; i < field.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < field.Cells.GetLength(1); j++)
                {
                    field.Cells[i, j].Update();
                }
            }

            for (var i = 0; i < field.Cells.GetLength(0); i++)
            {
                for (var j = 0; j < field.Cells.GetLength(1); j++)
                {
                    if (field.Cells[i, j].IsAlive)
                        field.AddCell(i, j);
                    else
                        field.RemoveCell(i, j);
                }
            }
        }
    }
}
