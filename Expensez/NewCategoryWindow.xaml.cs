﻿using System;
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
    /// Interaction logic for NewCategoryWindow.xaml
    /// </summary>
    public partial class NewCategoryWindow : Window {
        public NewCategoryWindow() {
            InitializeComponent();
        }

        public string CategoryName => _categoryName.Text.Trim();

        private void OnOK(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
        }

        private void OnCancel(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
        }
    }
}
