using System.ComponentModel;
using System.Windows.Input;
using Expensez.Commands;

namespace Expensez;

public class CategorizationPresentation : INotifyPropertyChanged
{    
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ICommand _newCategoryCommand;

    public ICommand NewCategoryCommand => _newCategoryCommand;

    public CategorizationPresentation()
    {
        _newCategoryCommand = new NewCategoryCommand(this);
    }
}
