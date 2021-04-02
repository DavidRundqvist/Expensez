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

            var categoryExpenses = from expense in expenses
                                   let matchingCategory = categories.Concat(new[] { Constants.DefaultCategory.Category }).First(c => c.IsMatch(expense))
                                   let categoryName = matchingCategory.Name
                                   select (expense, categoryName);
            var groups = categoryExpenses
                .OrderBy(c => c.categoryName)
                .GroupBy(c => c.categoryName)
                .Select(g => (Category: g.Key, Expenses: g.Select(e => e.expense).ToArray()));

            var results = groups.Select(g => new CategoryResultsPresentation(g.Category, g.Expenses));

            Categories.AddRange(results);                                
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CategoryResultsPresentation> Categories { get; } = new ObservableCollection<CategoryResultsPresentation>();

        public string Header => _year.ToString();
    }
}
