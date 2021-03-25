using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;

namespace Expensez {
    public class ExpensePresentation : INotifyPropertyChanged {

        public const string DefaultCategory = "Övrigt";

        private readonly Expense _expense;
        private string _category = DefaultCategory;

        public ExpensePresentation(Expense expense) {
            _expense = expense;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public string Date => _expense.Date.ToShortDateString();

        public string Recipient => _expense.Recipient;

        public decimal Amount => _expense.Amount;

        public bool IsCategorized => this.Category != DefaultCategory;

        public string Color => IsCategorized ? Colors.LightGreen.ToString() : Colors.White.ToString();

        public string Category { 
            get => _category;
            set {
                if (_category != value) {
                    _category = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Category)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
                }
            }
        }
        public bool IsSelected { get; set; } = false;

    }
}
