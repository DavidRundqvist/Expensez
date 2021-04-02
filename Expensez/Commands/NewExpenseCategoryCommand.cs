using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Expensez.Commands {
    public class NewExpenseCategoryCommand : ICommand, INotifyPropertyChanged {
        private readonly MainPresentation _mainPresentation;

        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public NewExpenseCategoryCommand(MainPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public string Header => "New...";

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            var selectedExpenses = _mainPresentation.SelectedExpenses;
            if (!selectedExpenses.Any())
                return;

            var recipients = selectedExpenses.Select(e => e.Recipient);

            var dlg = new EditCategoryWindow {
                CategoryName = "",
                Patterns = recipients.ToArray()
            };
            if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                var category = new Category(dlg.CategoryName, dlg.Color, dlg.Patterns);
                _mainPresentation.AddCategory(category);
            }
        }
    }
}
