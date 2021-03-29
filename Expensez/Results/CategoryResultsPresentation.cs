using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez.Results {
    public class CategoryResultsPresentation : INotifyPropertyChanged {

        public CategoryResultsPresentation(string category, Expense[] expenses) {
            Name = category;

            Jan = CalculateMonth(1, expenses);
            Feb = CalculateMonth(2, expenses);
            Mar = CalculateMonth(3, expenses);
            Apr = CalculateMonth(4, expenses);
            May = CalculateMonth(5, expenses);
            Jun = CalculateMonth(6, expenses);
            Jul = CalculateMonth(7, expenses);
            Aug = CalculateMonth(8, expenses);
            Sep = CalculateMonth(9, expenses);
            Oct = CalculateMonth(10, expenses);
            Nov = CalculateMonth(11, expenses);
            Dec = CalculateMonth(12, expenses);


        }

        private string CalculateMonth(int month, Expense[] expenses) {
            var totalCost = expenses
                .Where(e => e.Date.Month == month)
                .Sum(e => -e.Amount);
            return $"{totalCost:0}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; }

        public string Jan { get; } 
        public string Feb { get; } 
        public string Mar { get; } 
        public string Apr { get; }
        public string May { get; }
        public string Jun { get; }
        public string Jul { get; }
        public string Aug { get; }
        public string Sep { get; }
        public string Oct { get; }
        public string Nov { get; }
        public string Dec { get; }


    }
}
