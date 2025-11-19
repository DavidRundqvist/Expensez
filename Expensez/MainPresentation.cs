using System.ComponentModel;

namespace Expensez;

public class MainPresentation : INotifyPropertyChanged
{
    public MainPresentation(ExpenseRepository expenseRepository, CategoryRepository categoryRepository)
    {
        Categorization = new CategorizationPresentation(categoryRepository);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CategorizationPresentation Categorization {get;}
    
}