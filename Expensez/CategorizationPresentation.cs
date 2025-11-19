using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Expensez.Commands;

namespace Expensez;

public class CategorizationPresentation : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ICommand _newCategoryCommand;

    public ICommand NewCategoryCommand => _newCategoryCommand;
    public ObservableCollection<CategoryPresentation> Categories { get; } = [];

    public CategorizationPresentation(CategoryRepository categoryRepository)
    {
        _newCategoryCommand = new NewCategoryCommand(this);
        
        Categories.AddRange(categoryRepository.Load().Select(c => new CategoryPresentation(c)));


        // Categories = [
        //     new(new("Mat", "Blue")),
        //     new(new("Bil", "Orange")),
        //     new(new("Kläder", "Green"))];
    }
}
