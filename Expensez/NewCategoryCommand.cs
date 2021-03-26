using System;
using System.Windows.Input;

namespace Expensez {
    public class NewCategoryCommand : ICommand {
        private readonly MainPresentation _mainPresentation;

        public event EventHandler CanExecuteChanged;

        public string Header { get; } = "New...";

        public NewCategoryCommand(MainPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            var dlg = new NewCategoryWindow();
            if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                _mainPresentation.SetCategory(dlg.CategoryName);
            }
        }
    }
}
