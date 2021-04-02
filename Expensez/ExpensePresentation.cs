using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace Expensez {
    public class ExpensePresentation : INotifyPropertyChanged {


        private readonly Expense _expense;
        private CategoryPresentation _category = Constants.DefaultCategory;

        public ExpensePresentation(Expense expense) {
            _expense = expense;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string Date => _expense.Date.ToShortDateString();

        public string Recipient => _expense.Recipient;

        public decimal Amount => _expense.Amount;

        public Expense Expense => _expense;

        public CategoryPresentation Category { 
            get => _category;
            set {
                if (_category != value) {
                    _category = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCategorized)));
                }
            }
        }
        public bool IsSelected { get; set; } = false;

        public bool IsCategorized => Category != Constants.DefaultCategory;

    }
}
