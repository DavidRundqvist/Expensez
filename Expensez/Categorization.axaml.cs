using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Expensez;

public partial class Categorization : UserControl
{
    public Categorization()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void OnExpenseDoubleTapped(object? sender, RoutedEventArgs e)
    {
        if (sender is not DataGrid grid || grid.SelectedItem is not ExpensePresentation expense)
            return;

        var vm = DataContext as CategorizationPresentation;
        if (vm is null)
            return;

        await vm.AssignExpenseToCategoryAsync(expense);
    }
}
