using Avalonia.Controls;

namespace Expensez;

public partial class MainWindow : Window
{
    public MainWindow(MainPresentation presentation)
    {
        InitializeComponent();
        this.DataContext = presentation;
        this.Loaded += (s,e) => presentation.Load();
    }
}