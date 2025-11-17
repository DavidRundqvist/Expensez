using System;
using System.Windows.Input;

namespace Expensez.Commands {
    public class NewCategoryCommand : ICommand {
        private readonly CategorizationPresentation _mainPresentation;

        public event EventHandler? CanExecuteChanged;

        public NewCategoryCommand(CategorizationPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public bool CanExecute(object? parameter) {
            return true;
        }

        public void Execute(object? parameter) {
            // var dlg = new EditCategoryWindow();
            // if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
            //     var category = new Category(dlg.CategoryName, dlg.Color, dlg.Patterns);
            //     _mainPresentation.AddCategory(category);
            // }
        }
    }
}
