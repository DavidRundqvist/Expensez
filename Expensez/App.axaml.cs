using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace Expensez;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainPresentation = new MainPresentation(new ExpenseRepository(), new CategoryRepository());
            desktop.MainWindow = new MainWindow(mainPresentation);
            mainPresentation.Categorization.Owner = desktop.MainWindow;
            mainPresentation.Results.Owner = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}