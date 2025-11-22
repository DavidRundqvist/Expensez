using System;
using System.Linq;
using System.Windows.Input;
using Avalonia.Media;

namespace Expensez.Commands {
    public class EditCategoryCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;

        public EditCategoryCommand(CategorizationPresentation mainPresentation)
        {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "Edit...";

        public override async void Execute(object? parameter) {
            if (parameter is not CategoryPresentation category) {
                return;
            }

            var dlg = new EditCategoryWindow {
                CategoryName = category.Name,
                Patterns = category.Patterns,
                Color = Color.Parse(category.Color),
            };
            if (await dlg.ShowDialog<bool>(_mainPresentation.Owner!) == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                category.Name = dlg.CategoryName;
                category.Color = dlg.Color.ToString();
                category.Patterns = dlg.Patterns;
                _mainPresentation.SaveCategories();
            }
        }
    }

}
