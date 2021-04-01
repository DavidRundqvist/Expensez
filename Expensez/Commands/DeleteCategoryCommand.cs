using System;
using System.Windows;
using System.Windows.Input;

namespace Expensez.Commands {
    public class DeleteCategoryCommand : ICommand {
        private readonly MainPresentation _mainPresentation;

        public event EventHandler CanExecuteChanged;

        public DeleteCategoryCommand(MainPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if (parameter is not CategoryPresentation category) {
                return;
            }

            var result = MessageBox.Show($"Delete category {category.Name}?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) {
                _mainPresentation.DeleteCategory(category);
            }
        }
    }
}
