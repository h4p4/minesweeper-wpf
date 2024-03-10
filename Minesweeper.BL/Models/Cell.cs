namespace Minesweeper.BL.Models
{
    public class Cell
    {
        public IEnumerable<Cell> NeighborCells;
        public bool IsFlagged { get; private set; }
        public bool IsRevealed { get; private set; }
        public int MinesAroundCount => NeighborCells?.Count(cell => cell is MineCell) ?? -1;

        public void Initialize(IEnumerable<Cell> neighborCells)
        {
            var neighborCellsList = neighborCells.ToList();
            ////TODO: вернуть
            if (neighborCellsList.Count() < 3 ||
                neighborCellsList.Count() > 8)
                throw new ArgumentException("Неверное количество соседствующих клеток.");

            NeighborCells = neighborCellsList;
            Revealed += OnRevealed;
        }

        public virtual void Reveal()
        {
            if (IsRevealed || IsFlagged)
                return;

            IsRevealed = true;
            Revealed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Revealed;

        public void SetFlag()
        {
            IsFlagged = !IsFlagged;
        }

        private void OnRevealed(object? sender, EventArgs e)
        {
            if (!(sender is Cell cell))
                return;

            if (cell.MinesAroundCount != 0)
                return;
            foreach (var neighborCell in cell.NeighborCells)
            {
                if (neighborCell is MineCell)
                    continue;
                neighborCell.Reveal();
            }
        }
    }
}