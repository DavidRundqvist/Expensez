using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Input.Platform;

namespace Expensez.Results {
    public class YearResultsPresentation : INotifyPropertyChanged {
        private readonly int _year;

        public YearResultsPresentation(int year, Expense[] expenses, Categorizer categorizer, IClipboard clipboard) {
            _year = year;
            Clipboard = clipboard;
            var allCategories = categorizer.GetAllCategories();
            var categorization = categorizer.Categorize(expenses);

            var results = allCategories
                .Select(c => new CategoryResultsPresentation(c.Name, 
                    categorization
                    .Where(i => i.Category == c)
                    .Select(i => i.Expense).ToArray()));

            Categories.AddRange(results);    
            CopyCommand = new CopyCommand(this);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<CategoryResultsPresentation> Categories { get; } = new ObservableCollection<CategoryResultsPresentation>();

        public string Header => _year.ToString();

        public IClipboard Clipboard { get; }

        public BaseCommand CopyCommand {get;}
    }
}
