using System;
using System.ComponentModel;

namespace Expensez {
    public class CategoryPresentation : INotifyPropertyChanged {
        private readonly Category _category;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public CategoryPresentation(Category category) {
            _category = category;
        }

        public string Name {
            get => _category.Name;
            set {
                if (_category.Name != value) {
                    _category.Name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public bool IsSelected { get; set; } = false;


        public string Color {
            get => _category.Color;
            set {
                if (_category.Color != value) {
                    _category.Color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Color)));
                }
            }
        }

        public string[] Patterns {
            get => _category.Patterns;
            set => _category.Patterns = value;
        }
        public Category Category => _category;

        internal bool IsMatch(ExpensePresentation e) {
            return _category.IsMatch(e.Expense);
        }
    }
}
