namespace Minesweeper.BL.Models
{
    internal class Field
    {
        public Cell[,] Cells { get; internal set; }

        public Cell this[int verticalIndex, int horizontalIndex]
        {
            get => Cells[verticalIndex, horizontalIndex];
            set => Cells[verticalIndex, horizontalIndex] = value;
        }
    }
}