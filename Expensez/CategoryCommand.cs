using System;
using System.Windows.Input;

namespace Expensez {

    public class CategoryCommand : ICommand {
        private readonly MainPresentation _mainPresentation;

        public event EventHandler CanExecuteChanged;

        public string Header { get; }

        public CategoryCommand(string header, MainPresentation mainPresentation) {
            Header = header;
            _mainPresentation = mainPresentation;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _mainPresentation.SetCategory(Header);
        }
    }
}
