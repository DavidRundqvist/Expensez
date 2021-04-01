using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Expensez.Commands {
    public class CategorizeExpenseCommand : ICommand, INotifyPropertyChanged {
        private readonly MainPresentation _mainPresentation;
        private readonly CategoryPresentation _category;

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public CategorizeExpenseCommand(MainPresentation mainPresentation, CategoryPresentation category) {
            _mainPresentation = mainPresentation;
            _category = category;
            _category.PropertyChanged += (s, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Header)));
        }

        public string Header => _category.Name;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            var selectedExpenses = _mainPresentation.SelectedExpenses;
            if (!selectedExpenses.Any())
                return;

            var recipients = selectedExpenses.Select(e => e.Recipient);

            var dlg = new EditCategoryWindow {
                CategoryName = _category.Name,
                Patterns = _category.Patterns.Concat(recipients).ToArray()
            };
            if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                _category.Name = dlg.CategoryName;
                _category.Patterns = dlg.Patterns;
                _mainPresentation.SaveCategories();
            }
        }
    }
}
