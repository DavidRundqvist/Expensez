using System.ComponentModel;

namespace Expensez;

public class MainPresentation : INotifyPropertyChanged
{
    public MainPresentation()
    {
        Categorization = new CategorizationPresentation();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CategorizationPresentation Categorization {get;}
    
}