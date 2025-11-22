using System;
using System.Linq;
using System.Windows.Input;

namespace Expensez.Commands {
    public class EditCategoryCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;

        public EditCategoryCommand(CategorizationPresentation mainPresentation)
        {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "Edit...";

        public override void Execute(object? parameter) {
            if (parameter is not CategoryPresentation category) {
                return;
            }

            // var dlg = new EditCategoryWindow {
            //     CategoryName = category.Name,
            //     Patterns = category.Patterns,
            //     Color = category.Color,
            // };
            // if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
            //     category.Name = dlg.CategoryName;
            //     category.Color = dlg.Color;
            //     category.Patterns = dlg.Patterns;
            //     _mainPresentation.SaveCategories();
            // }
        }
    }

}
