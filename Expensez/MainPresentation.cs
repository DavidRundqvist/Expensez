using System;
using System.ComponentModel;

namespace Expensez;

public class MainPresentation : INotifyPropertyChanged
{
    public MainPresentation(ExpenseRepository expenseRepository, CategoryRepository categoryRepository)
    {
        Categorization = new CategorizationPresentation(expenseRepository, categoryRepository);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CategorizationPresentation Categorization {get;}

    internal void Load()
    {
        Categorization.Load();
    }
}