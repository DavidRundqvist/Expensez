using Avalonia.Controls;
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
}

