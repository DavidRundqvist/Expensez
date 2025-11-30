using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Expensez.Commands;

namespace Expensez;

public class CategorizationPresentation : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ExpenseRepository _expenseRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly Categorizer _categorizer;
    public BaseCommand NewCategoryCommand {get;}
    public BaseCommand EditCategoryCommand {get;}
    public BaseCommand DeleteCategoryCommand {get;}
    public BaseCommand NewExpenseCategoryCommand {get;}
    public ObservableCollection<CategoryPresentation> Categories { get; } = [];
    public ObservableCollection<ExpensePresentation> Expenses { get; } = [];
    public ObservableCollection<BaseCommand> CategoryCommands { get; } = [];

    public ExpensePresentation[] SelectedExpenses => [.. Expenses.Where(e => e.IsSelected)];


    public Window? Owner { get; set; } = null;

    public CategorizationPresentation(ExpenseRepository expenseRepository, CategoryRepository categoryRepository, Categorizer categorizer)
    {
        NewCategoryCommand = new NewCategoryCommand(this);
        EditCategoryCommand = new EditCategoryCommand(this);
        DeleteCategoryCommand = new DeleteCategoryCommand(this);

        NewExpenseCategoryCommand = new NewExpenseCategoryCommand(this);    

        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
        _categorizer = categorizer;
        Categories.CollectionChanged += CategoriesChanged;
    }

    private void CategoriesChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        CategoryCommands.Clear();
        CategoryCommands.AddRange(Categories.Select(c => new CategorizeExpenseCommand(this, c)).Concat([NewExpenseCategoryCommand]));
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

    internal void AddCategory(Category category)
    {
        _categoryRepository.Add(category);
        Categories.Add(new CategoryPresentation(category));
        _categorizer.Categorize(Expenses);
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

