using System;
using System.Windows.Input;

namespace Expensez.Commands {
    public class NewCategoryCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;


        public NewCategoryCommand(CategorizationPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "New...";

        public override async void Execute(object? parameter) {
            var dlg = new EditCategoryWindow();
            if (await dlg.ShowDialog<bool>(_mainPresentation.Owner!) == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                var category = new Category(dlg.CategoryName, dlg.Color.ToString(), dlg.Patterns);
                _mainPresentation.AddCategory(category);
            }
        }
    }
}
