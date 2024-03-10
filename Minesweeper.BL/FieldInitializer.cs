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

            CreateCells();
            InitializeCells();

            return _field;
        }

        private bool AreValidIndices(int i, int j)
        {
            return i >= 0 && i <= VerticalCellsCount - 1 &&
                   j >= 0 && j <= HorizontalCellsCount - 1;
        }

        private void CreateCells()
        {
            var random = new Random();

            for (var i = 0; i < VerticalCellsCount; i++)
            {
                for (var j = 0; j < HorizontalCellsCount; j++)
                {
                    if (random.Next(1, 10) == 1)
                    {
                        _field[i, j] = new MineCell();
                        continue;
                    }

                    _field[i, j] = new Cell();
                }
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