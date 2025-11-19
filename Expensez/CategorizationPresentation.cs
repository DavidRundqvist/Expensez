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
    private readonly ExpenseRepository _expenseRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly Categorizer _categorizer;

    public ICommand NewCategoryCommand => _newCategoryCommand;
    public ObservableCollection<CategoryPresentation> Categories { get; } = [];
    public ObservableCollection<ExpensePresentation> Expenses { get; } = [];

    public CategorizationPresentation(ExpenseRepository expenseRepository, CategoryRepository categoryRepository)
    {
        _newCategoryCommand = new NewCategoryCommand(this);
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
        _categorizer = new Categorizer(categoryRepository);        
    }

    public void Load()
    {
        LoadCategories();
        LoadExpenses();
        _categorizer.Categorize(Expenses);
    }


    internal void DeleteCategory(CategoryPresentation category)
    {
        Categories.Remove(category);
        _categoryRepository.Delete(category.Category);
        _categorizer.Categorize(Expenses);
    }

    private void LoadCategories()
    {
        var categories = _categoryRepository.Load();
        Categories.Clear();
        Categories.AddRange(categories.Select(c => new CategoryPresentation(c)));
    }

    internal void SaveCategories()
    {
        _categoryRepository.Save();
        _categorizer.Categorize(Expenses);
    }

    private void LoadExpenses()
    {
        var expenses = _expenseRepository.Load();
        var presentations = expenses.Select(e => new ExpensePresentation(e)).ToArray();
        Expenses.Clear();
        Expenses.AddRange(presentations);
    }
}

