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
        private IDictionary<string, string> _categories;

        public YearResultsPresentation(int year, Expense[] expenses, IDictionary<string, string> categories) {
            _year = year;
            _expenses = expenses;
            _categories = categories;

            var categoryExpenses = from expense in expenses
                                   let category = categories.ContainsKey(expense.Recipient) ? categories[expense.Recipient] : "Övrigt"
                                   select (expense, category);
            var groups = categoryExpenses
                .OrderBy(c => c.category)
                .GroupBy(c => c.category)
                .Select(g => (Category: g.Key, Expenses: g.Select(e => e.expense).ToArray()));

            var results = groups.Select(g => new CategoryResultsPresentation(g.Category, g.Expenses));

            Categories.AddRange(results);                                
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CategoryResultsPresentation> Categories { get; } = new ObservableCollection<CategoryResultsPresentation>();

        public string Header => _year.ToString();
    }
}
