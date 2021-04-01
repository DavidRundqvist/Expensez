using Expensez.Commands;
using Expensez.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Expensez {


    public class MainPresentation : INotifyPropertyChanged {
        private readonly ExpenseRepository _repository;
        private readonly CategoryRepository _categories;
        private readonly ICommand _newCategoryCommand;
        private readonly ICommand _editCategoryCommand;
        private readonly ICommand _deleteCategoryCommand;
        private readonly ICommand _newExpenseCategoryCommand;
        private readonly ResultsPresentation _results;

        public MainPresentation(ExpenseRepository repository, CategoryRepository categories) {
            _repository = repository;
            _categories = categories;
            _newCategoryCommand = new NewCategoryCommand(this);
            _editCategoryCommand = new EditCategoryCommand(this);
            _deleteCategoryCommand = new DeleteCategoryCommand(this);
            _newExpenseCategoryCommand = new NewExpenseCategoryCommand(this);
            _results = new ResultsPresentation(repository, categories);
            Categories.CollectionChanged += CategoriesChanged;
        }

        public ObservableCollection<ExpensePresentation> Expenses { get; } = new ObservableCollection<ExpensePresentation>();
        public ObservableCollection<CategoryPresentation> Categories { get; } = new ObservableCollection<CategoryPresentation>();

        public ObservableCollection<ICommand> CategoryCommands { get; } = new ObservableCollection<ICommand>();

        public ICommand NewCategoryCommand => _newCategoryCommand;
        public ICommand EditCategoryCommand => _editCategoryCommand;
        public ICommand DeleteCategoryCommand => _deleteCategoryCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExpensePresentation[] SelectedExpenses => Expenses.Where(e => e.IsSelected).ToArray();

        public ResultsPresentation Results => _results;

        private void CategoriesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            CategoryCommands.Clear();
            CategoryCommands.AddRange(Categories.Select(c => new CategorizeExpenseCommand(this, c)).Concat(new[] { _newExpenseCategoryCommand }));
        }

        internal void AddCategory(Category category) {
            _categories.Add(category);
            Categories.Add(new CategoryPresentation(category));
            CategorizeExpenses();
        }

        private void CategorizeExpenses() {
            foreach(var e in Expenses) {
                CategorizeExpense(e);
            }
        }

        private void CategorizeExpense(ExpensePresentation e) {
            var matchingCategory = Categories.FirstOrDefault(c => c.IsMatch(e));
            e.Category = matchingCategory?.Name ?? ExpensePresentation.DefaultCategory;
        }

        public void Load() {
            LoadCategories();
            LoadExpenses();
            CategorizeExpenses();
        }

        internal void DeleteCategory(CategoryPresentation category) {
            Categories.Remove(category);
            _categories.Delete(category.Category);
            CategorizeExpenses();
        }

        private void LoadCategories() {
            var categories = _categories.Load();
            Categories.Clear();
            Categories.AddRange(categories.Select(c => new CategoryPresentation(c)));
        }

        internal void SaveCategories() {
            _categories.Save();
            CategorizeExpenses();
        }

        private void LoadExpenses() {
            var expenses = _repository.Load();
            var presentations = expenses.Select(e => new ExpensePresentation(e)).ToArray();
            Expenses.Clear();
            Expenses.AddRange(presentations);
        }
    }
}
