using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez.Results {
    public class ResultsPresentation : INotifyPropertyChanged {
        private readonly Categorizer _categorizer;
        private readonly ExpenseRepository _expenseRepository;

        public ResultsPresentation(Categorizer categorizer, ExpenseRepository expenseRepository) {
            _categorizer = categorizer;
            _expenseRepository = expenseRepository;
        }

        public void CalculateResults() {
            Years.Clear();

            var expenses = _expenseRepository.Load();

            var expensesPerYear = expenses.GroupBy(e => e.Date.Year);
            var yearPresentations = expensesPerYear
                .Select(yearExpense => new YearResultsPresentation(yearExpense.Key, yearExpense.ToArray(), _categorizer));
            Years.AddRange(yearPresentations);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<YearResultsPresentation> Years { get; } = new ObservableCollection<YearResultsPresentation>();
    }
}
