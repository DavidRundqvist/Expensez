namespace Expensez.Commands {
    public class CategorizeExpenseCommand : BaseCommand {
        private readonly CategorizationPresentation _mainPresentation;
        private readonly CategoryPresentation _category;

        public CategorizeExpenseCommand(CategorizationPresentation mainPresentation, CategoryPresentation category) {
            _mainPresentation = mainPresentation;
            _category = category;
            _category.PropertyChanged += (s, e) => OnPropertyChange(nameof(Header));
        }

        public override string Header => _category.Name;

        public override void Execute(object? parameter) {
            // var selectedExpenses = _mainPresentation.SelectedExpenses;
            // if (!selectedExpenses.Any())
            //     return;

            // var recipients = selectedExpenses.Select(e => e.Recipient);

            // var dlg = new EditCategoryWindow {
            //     CategoryName = _category.Name,
            //     Color = _category.Color,
            //     Patterns = _category.Patterns.Concat(recipients).ToArray()
            // };
            // if (dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
            //     _category.Name = dlg.CategoryName;
            //     _category.Color = dlg.Color;
            //     _category.Patterns = dlg.Patterns;               
            //     _mainPresentation.SaveCategories();
            // }
        }
    }
}
