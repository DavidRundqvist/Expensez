using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;

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

        public override async void Execute(object? parameter) {
            if (!(parameter is ExpensePresentation recipient))
                return;

            var dlg = new EditCategoryWindow {
                CategoryName = _category.Name,
                Color = Color.Parse(_category.Color),
                Patterns = _category.Patterns.Concat([recipient.Recipient]).ToArray()
            };
            if (await dlg.ShowDialog<bool>(_mainPresentation.Owner!) == true && !string.IsNullOrEmpty(dlg.CategoryName)) {
                _category.Name = dlg.CategoryName;
                _category.Color = dlg.Color.ToString();
                _category.Patterns = dlg.Patterns;               
                _mainPresentation.SaveCategories();
            }
        }
    }
}
