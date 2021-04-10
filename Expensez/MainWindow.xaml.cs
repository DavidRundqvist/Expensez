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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Expensez {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = new MainPresentation(new ExpenseRepository(), new CategoryRepository());
            this.Loaded += (s, e) => Presentation.Load();
            NameScope.SetNameScope(_expensesMenu, NameScope.GetNameScope(this));
        }

        MainPresentation Presentation => this.DataContext as MainPresentation;

        private void DoubleClickCategory(object sender, MouseButtonEventArgs e) {
            Presentation.EditCategoryCommand.Execute(_categoriesList.SelectedItem);
        }
    }
}
