namespace Minesweeper.UI.Views
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    using Minesweeper.BL;
    using Minesweeper.BL.Models;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dictionary<Cell, Button> ButtonCellDictionary = new();
        private readonly MinesweeperGame _game = MinesweeperGame.Instance;

        public MainWindow()
        {
            InitializeComponent();

            CellsGrid.Columns = _game.FieldWidth;
            CellsGrid.Rows = _game.FieldHeight;

            for (var i = 0; i < _game.FieldHeight; i++)
            {
                for (var j = 0; j < _game.FieldWidth; j++)
                    Initialize(i, j);
            }

            void Initialize(int i, int j)
            {
                var button = new Button();
                var cell = _game.GetCell(i, j);
                ButtonCellDictionary.Add(cell, button);

                UiConfigurator.SetCell(button, cell);
                Grid.SetRow(button, j);
                Grid.SetColumn(button, i);
                CellsGrid.Children.Add(button);
            }
        }
    }
}