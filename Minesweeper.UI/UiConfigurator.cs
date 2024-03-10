namespace Minesweeper.UI;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Minesweeper.BL;
using Minesweeper.BL.Enums;
using Minesweeper.BL.Models;

public static class UiConfigurator
{
    private static SolidColorBrush revealedBrush = new(Color.FromRgb(178, 178, 178));
    private static SolidColorBrush hiddenBrush = new(Color.FromRgb(198, 198, 198));
    public static readonly DependencyProperty CellProperty = DependencyProperty.RegisterAttached(
        "Cell", typeof(Cell), typeof(Button));

    public static Cell GetCell(Button element)
    {
        return (Cell)element.GetValue(CellProperty);
    }

    public static void SetCell(Button button, Cell cell)
    {
        button.Click += ButtonOnClick;
        button.MouseRightButtonUp += ButtonOnMouseRightButtonUp;
        button.Background = hiddenBrush;

        cell.Revealed += (sender, args) =>
        {
            Brush brush = null;
            button.Background = revealedBrush;
            switch (cell.MinesAroundCount)
            {
                case 1:
                    brush = Brushes.Blue;
                    break;
                case 2:
                    brush = Brushes.Green;
                    break;
                case 3:
                    brush = Brushes.Red;
                    break;
                case 4:
                    brush = Brushes.DarkBlue;
                    break;
                case 5:
                    brush = Brushes.Brown;
                    break;
                case 6:
                    brush = Brushes.Turquoise;
                    break;
                case 7:
                    brush = Brushes.Black;
                    break;
                case 8:
                    brush = Brushes.White;
                    break;
            }

            if (cell.MinesAroundCount == 0)
                return;

            button.Content = cell.MinesAroundCount.ToString();
            button.Foreground = brush;
        };
        button.SetValue(CellProperty, cell);
    }

    private static void ButtonOnClick(object sender, RoutedEventArgs e)
    {
        if (!(sender is Button button))
            return;

        var cell = GetCell(button);

        if (cell.IsFlagged)
            return;
        try
        {
            cell.Reveal();
        }
        catch (GameOverException gameOverException)
        {
            MessageBox.Show(gameOverException.Message);
        }
    }

    private static void ButtonOnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (!(sender is Button button))
            return;

        var cell = GetCell(button);
        if (cell.IsRevealed)
            return;

        cell.SetFlag();
        var isFlagged = cell.IsFlagged;
        var content = isFlagged ? "F" : "";
        if (isFlagged)
            button.Foreground = Brushes.Red;
        button.Content = content;
    }
}