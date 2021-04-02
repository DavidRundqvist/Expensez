using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez.Results {
    public class YearResultsPresentation : INotifyPropertyChanged {
        private readonly int _year;

        public YearResultsPresentation(int year, Expense[] expenses, Category[] categories) {
            _year = year;

            var allCategories = categories.Concat(new[] { Constants.DefaultCategory.Category });
            var categorization = expenses.Select(e => (Expense: e, Category: allCategories.First(c => c.IsMatch(e)))).ToArray();

            var results = categories
                .Select(c => new CategoryResultsPresentation(c.Name, 
                    categorization
                    .Where(i => i.Category == c)
                    .Select(i => i.Expense).ToArray()));

            Categories.AddRange(results);                                
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CategoryResultsPresentation> Categories { get; } = new ObservableCollection<CategoryResultsPresentation>();

        public string Header => _year.ToString();
    }
}
