using Expensez.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Expensez {


    public class MainPresentation : INotifyPropertyChanged {
        private readonly ExpenseRepository _repository;
        private readonly CategoryRepository _categories;
        private readonly ICommand _newCategoryCommand;
        private readonly ResultsPresentation _results;

        public MainPresentation(ExpenseRepository repository, CategoryRepository categories) {
            _repository = repository;
            _categories = categories;
            _newCategoryCommand = new NewCategoryCommand(this);
            _results = new ResultsPresentation(repository, categories);
        }

        public void Load() {
            var categories = LoadCategories();
            var expenses = LoadExpenses();
            Categorize(expenses, categories);
        }

        private void Categorize(ExpensePresentation[] expenses, IDictionary<string, string> categories) {
            foreach(var expense in expenses) {
                if (categories.ContainsKey(expense.Recipient)) {
                    expense.Category = categories[expense.Recipient];
                }
            }
        }

        private IDictionary<string, string> LoadCategories() {

            var categories = _categories.Load();

            Categories.Clear();
            Categories.AddRange(_categories.AllCategories);

            CategoryCommands.Clear();
            CategoryCommands.AddRange(categories.Values.Distinct().Select(c => new CategoryCommand(c, this)));
            CategoryCommands.Add(_newCategoryCommand);
            return categories;
        }

        private ExpensePresentation[] LoadExpenses() {
            var expenses = _repository.Load();
            var presentations = expenses.Select(e => new ExpensePresentation(e)).ToArray();
            Expenses.Clear();
            Expenses.AddRange(presentations);
            return presentations;
        }


        public ObservableCollection<ExpensePresentation> Expenses { get; } = new ObservableCollection<ExpensePresentation>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public ObservableCollection<ICommand> CategoryCommands { get; } = new ObservableCollection<ICommand>();


        public event PropertyChangedEventHandler PropertyChanged;

        public ExpensePresentation[] SelectedExpenses => Expenses.Where(e => e.IsSelected).ToArray();

        public ResultsPresentation Results => _results;

        public void SetCategory(string category) {
            foreach(var expense in SelectedExpenses) {
                SetCategory(expense.Recipient, category);
            }
            _categories.SetCategory(category, SelectedExpenses.Select(e => e.Recipient).ToArray());
            LoadCategories();
        }

        public void SetCategory(string recipient, string category) {
            var expenses = Expenses.Where(e => e.Recipient == recipient).ToArray();
            foreach(var expense in expenses) {
                expense.Category = category;
            }
        }
    }
}
