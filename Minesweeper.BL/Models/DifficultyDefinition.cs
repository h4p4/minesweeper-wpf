namespace Minesweeper.BL.Models
{
    internal class DifficultyDefinition
    {
        public DifficultyDefinition(int mineCount, int verticalCellsCount, int horizontalCellsCount)
        {
            MineCount = mineCount;
            VerticalCellsCount = verticalCellsCount;
            HorizontalCellsCount = horizontalCellsCount;
        }

        public int MineCount { get; }
        public int VerticalCellsCount { get; }
        public int HorizontalCellsCount { get; }
    }
}