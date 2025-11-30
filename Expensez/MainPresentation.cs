using System;
using System.ComponentModel;
using Expensez.Results;

namespace Expensez;

public class MainPresentation : INotifyPropertyChanged
{
    public MainPresentation(ExpenseRepository expenseRepository, CategoryRepository categoryRepository)
    {
        var categorizer = new Categorizer(categoryRepository);
        Categorization = new CategorizationPresentation(expenseRepository, categoryRepository, categorizer);
        Results = new ResultsPresentation(categorizer, expenseRepository);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public CategorizationPresentation Categorization {get;}
    public ResultsPresentation Results {get;}

    internal void Load()
    {
        Categorization.Load();
    }
}