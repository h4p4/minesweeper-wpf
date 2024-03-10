namespace Minesweeper.BL.Models
{
    public class MineCell : Cell
    {
        public override void Reveal()
        {
            if (IsFlagged)
                return;

            var score = MinesweeperGame.Instance.Score;
            throw new GameOverException(score);
        }
    }
}