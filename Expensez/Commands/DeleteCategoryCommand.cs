using System;
using System.Windows;
using System.Windows.Input;

namespace Expensez.Commands {
    public class DeleteCategoryCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;

        public DeleteCategoryCommand(CategorizationPresentation mainPresentation)
        {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "Delete";

        public override void Execute(object? parameter) {
            if (parameter is not CategoryPresentation category) {
                return;
            }

            // var result = MessageBox.Show($"Delete category {category.Name}?", "Delete", MessageBoxButton.YesNo);
            // if (result == MessageBoxResult.Yes) {
            //     _mainPresentation.DeleteCategory(category);
            // }
        }
    }
}
