using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Expensez {
    /// <summary>
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window {
        private CategoryPresentation[] _assignableCategories = [];
        private string[] _suggestedPatterns = [];

        public EditCategoryWindow() {
            InitializeComponent();
        }

        public string CategoryName { 
            get => _categoryName.Text?.Trim() ?? "";
            set => _categoryName.Text = value.Trim();        
        }

        public string SelectedCategoryName
        {
            get => _categoryPicker.SelectedItem as string ?? CategoryName;
            set
            {
                if (_categoryPicker.ItemsSource is string[] options)
                {
                    var index = Array.IndexOf(options, value);
                    _categoryPicker.SelectedIndex = index >= 0 ? index : 0;
                }
                else
                {
                    _categoryPicker.SelectedItem = value;
                }
            }
        }

        public string[] Patterns { 
            get => _patterns.Text?.Trim().Split(Environment.NewLine).Where(s => !string.IsNullOrEmpty(s)).ToArray() ?? [];
            set => _patterns.Text = string.Join(Environment.NewLine, value).Trim();
        }

        public Color Color {
            get => _colorPicker.Color;
            set => _colorPicker.Color = value;
        }

        public void ConfigureCategoryAssignment(CategoryPresentation[] categories, CategoryPresentation selectedCategory, string[] suggestedPatterns)
        {
            _assignableCategories = categories;
            _suggestedPatterns = suggestedPatterns;
            _categoryPicker.ItemsSource = categories.Select(c => c.Name).ToArray();
            _categoryPicker.SelectedIndex = Array.IndexOf(categories.Select(c => c.Name).ToArray(), selectedCategory.Name);
            if (_categoryPicker.SelectedIndex < 0)
                _categoryPicker.SelectedIndex = 0;

            _categoryPicker.IsVisible = true;
            _categoryPickerLabel.IsVisible = true;
            RefreshCategoryFields();
        }

        private void OnCategorySelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            RefreshCategoryFields();
        }

        private void RefreshCategoryFields()
        {
            var selectedName = SelectedCategoryName;
            var selectedCategory = _assignableCategories.FirstOrDefault(c => c.Name == selectedName);
            if (selectedCategory is null && _assignableCategories.Length > 0)
                selectedCategory = _assignableCategories[0];

            if (selectedCategory is null)
                return;

            CategoryName = selectedCategory.Name;
            Color = Color.Parse(selectedCategory.Color);
            Patterns = selectedCategory.Patterns
                .Concat(_suggestedPatterns)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Distinct()
                .ToArray();
        }

        private void OnOK(object sender, RoutedEventArgs e) {
            this.Close(true);
        }

        private void OnCancel(object sender, RoutedEventArgs e) {
            this.Close(false);
        }
    }
}
