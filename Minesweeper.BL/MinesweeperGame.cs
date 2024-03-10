namespace Minesweeper.BL
{
    using Minesweeper.BL.Enums;
    using Minesweeper.BL.Models;

    public class MinesweeperGame
    {
        private readonly Field _field;

        private MinesweeperGame(Difficulty difficulty)
        {
            var fieldInitializer = new FieldInitializer();
            _field = fieldInitializer.Initialize(difficulty);
        }

        public int FieldHeight => _field.Cells.GetLength(0);
        public int FieldWidth => _field.Cells.GetLength(1);
        public static MinesweeperGame Instance { get; private set; }
        public int Score { get; private set; }

        public Cell GetCell(int verticalIndex, int horizontalIndex)
        {
            return _field[verticalIndex, horizontalIndex];
        }

        public static void Initialize(Difficulty difficulty)
        {
            Instance = new MinesweeperGame(difficulty);
        }
    }
}