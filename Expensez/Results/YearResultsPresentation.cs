using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez.Results {
    public class YearResultsPresentation : INotifyPropertyChanged {
        private int _year;
        private Expense[] _expenses;
        private Category[] _categories;

        public YearResultsPresentation(int year, Expense[] expenses, Category[] categories) {
            _year = year;
            _expenses = expenses;
            _categories = categories;

            var categoryExpenses = from expense in expenses
                                   let matchingCategory = _categories.FirstOrDefault(c => c.IsMatch(expense))
                                   let categoryName = matchingCategory?.Name ?? ExpensePresentation.DefaultCategory
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
