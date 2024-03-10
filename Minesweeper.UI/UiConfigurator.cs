namespace Minesweeper.UI;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Minesweeper.BL.Models;

public static class UiConfigurator
{
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
        cell.Revealed += (sender, args) =>
        {
            button.Content = cell.MinesAroundCount.ToString();
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

        cell.Reveal();
        var content = cell is MineCell ? "" : cell.MinesAroundCount.ToString();
        button.Content = content;
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
        {
            button.Foreground = Brushes.Red;
        }
        button.Content = content;
    }
}