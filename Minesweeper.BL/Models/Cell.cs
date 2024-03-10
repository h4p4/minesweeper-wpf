namespace Minesweeper.BL.Models
{
    public class Cell
    {
        private IEnumerable<Cell> _neighborCells;

        public bool CanRevealNeighbors
        {
            get
            {
                if (_neighborCells.Count(cell => !cell.IsRevealed) == 0)
                    return false;
                if (FlagsAroundCount == 0 && MinesAroundCount == 0)
                    return false;
                return MinesAroundCount - FlagsAroundCount == 0 && IsRevealed;
            }
        }

        public int FlagsAroundCount => _neighborCells?.Count(cell => cell.IsFlagged) ?? -1;
        public bool IsFlagged { get; private set; }
        public bool IsMine { get; internal set; }
        public bool IsRevealed { get; private set; }
        public int MinesAroundCount => _neighborCells?.Count(cell => cell.IsMine) ?? -1;

        public void Initialize(IEnumerable<Cell> neighborCells)
        {
            var neighborCellsList = neighborCells.ToList();
            if (neighborCellsList.Count() < 3 ||
                neighborCellsList.Count() > 8)
                throw new ArgumentException("Неверное количество соседствующих клеток.");

            _neighborCells = neighborCellsList;
        }

        public void Reveal()
        {
            Reveal(true);
        }

        public event EventHandler Revealed;

        public void SetFlag()
        {
            IsFlagged = !IsFlagged;
        }

        private void Reveal(bool revealNeighbors)
        {
            if (IsMine)
                throw new GameOverException(MinesweeperGame.Instance.Score);

            if (IsRevealed && revealNeighbors)
            {
                if (CanRevealNeighbors)
                {
                    RevealNeighbors();
                    return;
                }

                return;
            }
            if (IsRevealed)
                return;

            if (IsFlagged)
                return;

            IsRevealed = true;
            Revealed?.Invoke(this, EventArgs.Empty);

            if (MinesAroundCount != 0)
                return;

            foreach (var neighborCell in _neighborCells)
            {
                if (neighborCell.IsMine)
                    continue;

                neighborCell.Reveal(false);
            }
        }


        private void RevealNeighbors()
        {
            foreach (var neighborCell in _neighborCells.Where(cell => !cell.IsMine))
            {
                neighborCell.Reveal(false);
            }
        }
    }
}