namespace Minesweeper.BL
{
    using Minesweeper.BL.Enums;
    using Minesweeper.BL.Models;

    internal static class Constants
    {
        public static readonly Dictionary<Difficulty, DifficultyDefinition> Difficulties = new()
        {
            { Difficulty.Easy , new DifficultyDefinition(10, 10, 10)},
            { Difficulty.Medium , new DifficultyDefinition(40, 16, 16)},
            { Difficulty.Hard , new DifficultyDefinition(99, 16, 30)},
        };
    }
}