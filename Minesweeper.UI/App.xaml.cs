namespace Minesweeper.UI
{
    using System.Windows;

    using Minesweeper.BL;
    using Minesweeper.BL.Enums;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MinesweeperGame.Initialize(Difficulty.Hard);
        }
    }
}