using System.Linq;

namespace Expensez.Commands {
    public class NewExpenseCategoryCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;


        public NewExpenseCategoryCommand(CategorizationPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "New...";


        public override async void Execute(object? parameter) {
            if (!(parameter is ExpensePresentation recipient))
                return;

            var dlg = new EditCategoryWindow {
                CategoryName = "",
                Patterns = [recipient.Recipient]
            };
            if (await dlg.ShowDialog<bool>(_mainPresentation.Owner!) && !string.IsNullOrEmpty(dlg.CategoryName)) {
                var category = new Category(dlg.CategoryName, dlg.Color, dlg.Patterns);
                _mainPresentation.AddCategory(category);
            }
        }
    }
}
