namespace Expensez.Commands {
    public class NewExpenseCategoryCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;


        public NewExpenseCategoryCommand(CategorizationPresentation mainPresentation) {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "New...";


        public override void Execute(object? parameter) {
            // var selectedExpenses = _mainPresentation.SelectedExpenses;
            // if (!selectedExpenses.Any())
            //     return;

            // var recipients = selectedExpenses.Select(e => e.Recipient);

            // var dlg = new EditCategoryWindow {
            //     CategoryName = "",
            //     Patterns = recipients.ToArray()
            // };
            // if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
            //     var category = new Category(dlg.CategoryName, dlg.Color, dlg.Patterns);
            //     _mainPresentation.AddCategory(category);
            // }
        }
    }
}
