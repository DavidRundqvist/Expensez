using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Expensez;

public partial class MainWindow : Window
{
    public MainWindow() : this(null!) {}

    public MainWindow(MainPresentation presentation)
    {
        InitializeComponent();
        this.DataContext = presentation;
        this.Loaded += (s,e) => presentation.Load();
    }
}