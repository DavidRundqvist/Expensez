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

        public EditCategoryWindow() {
            InitializeComponent();
        }

        public string CategoryName { 
            get => _categoryName.Text?.Trim() ?? "";
            set => _categoryName.Text = value.Trim();        
        }

        public string[] Patterns { 
            get => _patterns.Text?.Trim().Split(Environment.NewLine).Where(s => !string.IsNullOrEmpty(s)).ToArray() ?? [];
            set => _patterns.Text = string.Join(Environment.NewLine, value).Trim();
        }

        public Color Color {
            get => _colorPicker.Color;
            set => _colorPicker.Color = value;
        }

        private void OnOK(object sender, RoutedEventArgs e) {
            this.Close(true);
        }

        private void OnCancel(object sender, RoutedEventArgs e) {
            this.Close(false);
        }
    }
}
