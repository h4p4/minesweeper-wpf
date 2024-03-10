namespace Minesweeper.BL
{
    using Minesweeper.BL.Enums;
    using Minesweeper.BL.Models;

    internal class FieldInitializer
    {
        private DifficultyDefinition _difficultyDefinition;
        private Field _field;
        private int HorizontalCellsCount => _difficultyDefinition.HorizontalCellsCount;
        private int VerticalCellsCount => _difficultyDefinition.VerticalCellsCount;

        public Field Initialize(Difficulty difficulty)
        {
            _difficultyDefinition = GetDifficultyDefinition(difficulty);
            _field = new Field
            {
                Cells = new Cell[VerticalCellsCount, HorizontalCellsCount]
            };

            CreateCells(_difficultyDefinition.MineCount);
            InitializeCells();

            return _field;
        }

        private bool AreValidIndices(int i, int j)
        {
            return i >= 0 && i <= VerticalCellsCount - 1 &&
                   j >= 0 && j <= HorizontalCellsCount - 1;
        }

        private void CreateCells(int mineCount)
        {
            var notMineCells = new List<(int i, int j)>();
            mineCount++;
            for (var i = 0; i < VerticalCellsCount; i++)
            {
                for (var j = 0; j < HorizontalCellsCount; j++)
                {
                    _field[i, j] = new Cell();
                    notMineCells.Add((i, j));
                }
            }

            var random = new Random();
            while (mineCount != 0)
            {
                var next = random.Next(0, notMineCells.Count);
                var tuple = notMineCells[next];
                var randomCell = _field[tuple.i, tuple.j];
                notMineCells.Remove(tuple);
                randomCell.IsMine = true;
                mineCount--;
            }
        }

        private static DifficultyDefinition GetDifficultyDefinition(Difficulty difficulty)
        {
            Constants.Difficulties.TryGetValue(difficulty, out var difficultyDefinition);
            if (difficultyDefinition == null)
                throw new NotImplementedException($"Уровень сложности {difficultyDefinition} не реализован.");

            return difficultyDefinition;
        }

        private IEnumerable<Cell> GetNeighborCells(int i, int j)
        {
            // левая клетка
            if (AreValidIndices(--i, j))
                yield return _field[i, j];
            // левая верхняя клетка
            if (AreValidIndices(i, --j))
                yield return _field[i, j];
            // верхняя клетка
            if (AreValidIndices(++i, j))
                yield return _field[i, j];
            // правая верхняя клетка
            if (AreValidIndices(++i, j))
                yield return _field[i, j];
            // правая клетка
            if (AreValidIndices(i, ++j))
                yield return _field[i, j];
            // правая нижняя клетка
            if (AreValidIndices(i, ++j))
                yield return _field[i, j];
            // нижняя клетка
            if (AreValidIndices(--i, j))
                yield return _field[i, j];
            // нижняя левая клетка
            if (AreValidIndices(--i, j))
                yield return _field[i, j];
        }

        private void InitializeCells()
        {
            for (var i = 0; i < VerticalCellsCount; i++)
            {
                for (var j = 0; j < HorizontalCellsCount; j++)
                {
                    var neighborCells = GetNeighborCells(i, j);
                    _field[i, j].Initialize(neighborCells);
                }
            }
        }
    }
}