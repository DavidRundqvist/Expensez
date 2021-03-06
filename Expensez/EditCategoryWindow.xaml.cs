using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Expensez {
    /// <summary>
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window {
        public EditCategoryWindow() {
            InitializeComponent();
            var r = new Random();
            Color = System.Windows.Media.Color.FromRgb(
                (byte)r.Next(1, 255),              
                (byte)r.Next(1, 255), 
                (byte)r.Next(1, 233)).ToString();
        }

        public string CategoryName { 
            get => _categoryName.Text.Trim();
            set => _categoryName.Text = value.Trim();        
        }

        public string[] Patterns { 
            get => _patterns.Text.Trim().Split(Environment.NewLine).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            set => _patterns.Text = string.Join(Environment.NewLine, value).Trim();
        }

        public string Color {
            get => _colorPicker.SelectedColor?.ToString() ?? "white";
            set => _colorPicker.SelectedColor = (Color)ColorConverter.ConvertFromString(value);
        }


        private void OnOK(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }

        private void OnCancel(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }
    }
}
