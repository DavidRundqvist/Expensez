using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez.Results {
    public class ResultsPresentation : INotifyPropertyChanged {
        private ExpenseRepository _expenses;
        private CategoryRepository _categories;

        public ResultsPresentation(ExpenseRepository expenses, CategoryRepository categories) {
            _expenses = expenses;
            _categories = categories;
        }

        public void CalculateResults() {
            Years.Clear();

            var expenses = _expenses.Load();
            var categories = _categories.Load();

            var expensesPerYear = expenses.GroupBy(e => e.Date.Year);
            var yearPresentations = expensesPerYear.Select(yearExpense => new YearResultsPresentation(yearExpense.Key, yearExpense.ToArray(), categories));
            Years.AddRange(yearPresentations);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<YearResultsPresentation> Years { get; } = new ObservableCollection<YearResultsPresentation>();
    }
}
