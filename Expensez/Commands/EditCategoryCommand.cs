using System;
using System.Windows.Input;

namespace Expensez.Commands {
    public class EditCategoryCommand : ICommand {
        private readonly MainPresentation _mainPresentation;

        public event EventHandler CanExecuteChanged;

        public EditCategoryCommand(MainPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if (parameter is not CategoryPresentation category) {
                return;
            }

            var dlg = new EditCategoryWindow {
                CategoryName = category.Name,
                Patterns = category.Patterns,
                Color = category.Color,
            };
            if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                category.Name = dlg.CategoryName;
                category.Color = dlg.Color;
                category.Patterns = dlg.Patterns;
                _mainPresentation.SaveCategories();
            }
        }
    }

}
