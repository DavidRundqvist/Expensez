using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;


namespace Expensez.Results; 
/// <summary>
/// Interaction logic for ResultsView.xaml
/// </summary>
public partial class ResultsView : UserControl {
    public ResultsView() {
        InitializeComponent();
    }
    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }    

    private void CalculateClick(object sender, RoutedEventArgs e) {
        (this.DataContext as ResultsPresentation)?.CalculateResults();
    }
}
