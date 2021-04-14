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
        private readonly ExpenseRepository _expenseRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly Categorizer _categorizer;
        private readonly ICommand _newCategoryCommand;
        private readonly ICommand _editCategoryCommand;
        private readonly ICommand _deleteCategoryCommand;
        private readonly ICommand _newExpenseCategoryCommand;
        private readonly ResultsPresentation _results;

        public MainPresentation(ExpenseRepository expenseRepository, CategoryRepository categoryRepository) {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _categorizer = new Categorizer(categoryRepository);
            _newCategoryCommand = new NewCategoryCommand(this);
            _editCategoryCommand = new EditCategoryCommand(this);
            _deleteCategoryCommand = new DeleteCategoryCommand(this);
            _newExpenseCategoryCommand = new NewExpenseCategoryCommand(this);

            _results = new ResultsPresentation(_categorizer, _expenseRepository);
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

        public CategoryPresentation[] SelectedCategories => Categories.Where(e => e.IsSelected).ToArray();

        public ResultsPresentation Results => _results;

        private void CategoriesChanged(object sender, NotifyCollectionChangedEventArgs e) {
            CategoryCommands.Clear();
            CategoryCommands.AddRange(Categories.Select(c => new CategorizeExpenseCommand(this, c)).Concat(new[] { _newExpenseCategoryCommand }));
        }

        internal void AddCategory(Category category) {
            _categoryRepository.Add(category);
            Categories.Add(new CategoryPresentation(category));
            _categorizer.Categorize(Expenses);            
        }

        public void Load() {
            LoadCategories();
            LoadExpenses();
            _categorizer.Categorize(Expenses);
        }

        internal void DeleteCategory(CategoryPresentation category) {
            Categories.Remove(category);
            _categoryRepository.Delete(category.Category);
            _categorizer.Categorize(Expenses);
        }

        private void LoadCategories() {
            var categories = _categoryRepository.Load();
            Categories.Clear();
            Categories.AddRange(categories.Select(c => new CategoryPresentation(c)));
        }

        internal void SaveCategories() {
            _categoryRepository.Save();
            _categorizer.Categorize(Expenses);
        }

        private void LoadExpenses() {
            var expenses = _expenseRepository.Load();
            var presentations = expenses.Select(e => new ExpensePresentation(e)).ToArray();
            Expenses.Clear();
            Expenses.AddRange(presentations);
        }
    }
}
