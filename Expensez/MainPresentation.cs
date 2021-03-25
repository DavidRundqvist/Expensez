using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Expensez {


    public class MainPresentation : INotifyPropertyChanged {
        private readonly ExpenseRepository _repository;
        private readonly CategoryRepository _categories;

        public MainPresentation(ExpenseRepository repository, CategoryRepository categories) {
            _repository = repository;
            _categories = categories;
        }

        public void Load() {
            LoadCategories();
            LoadExpenses();
        }

        private void LoadCategories() {
            Categories.Clear();
            Categories.AddRange(_categories.AllCategories);

            CategoryCommands.Clear();
            CategoryCommands.AddRange(_categories.AllCategories.Select(c => new CategoryCommand(c.Name, this)));
        }

        private void LoadExpenses() {
            var expenses = _repository.Load();
            var presentations = expenses.Select(e => new ExpensePresentation(e));
            Expenses.Clear();
            Expenses.AddRange(presentations);
        }


        public ObservableCollection<ExpensePresentation> Expenses { get; } = new ObservableCollection<ExpensePresentation>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public ObservableCollection<CategoryCommand> CategoryCommands { get; } = new ObservableCollection<CategoryCommand>();


        public event PropertyChangedEventHandler PropertyChanged;

        public ExpensePresentation[] SelectedExpenses => Expenses.Where(e => e.IsSelected).ToArray();

        public void SetCategory(string category) {
            foreach(var expense in SelectedExpenses) {
                SetCategory(expense.Recipient, category);
            }
        }

        public void SetCategory(string recipient, string category) {
            var expenses = Expenses.Where(e => e.Recipient == recipient).ToArray();
            foreach(var expense in expenses) {
                expense.Category = category;
            }
        }
    }
}
