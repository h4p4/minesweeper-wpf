namespace Minesweeper.BL
{
    public class GameOverException : Exception
    {
        private readonly int _score;

        public GameOverException(int score)
        {
            _score = score;
        }

        public override string Message => $"Вы проиграли!\nСчёт: {_score}";
    }
}