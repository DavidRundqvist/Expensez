using System;
using System.Windows;
using System.Windows.Input;

namespace Expensez.Commands
{
    public class DeleteCategoryCommand : BaseCommand
    {
        private readonly CategorizationPresentation _mainPresentation;

        public DeleteCategoryCommand(CategorizationPresentation mainPresentation)
        {
            _mainPresentation = mainPresentation;
        }

        public override string Header => "Delete";

        public override async void Execute(object? parameter)
        {
            if (parameter is not CategoryPresentation category)
            {
                return;
            }

            if (await MessageBox.ShowDialog(_mainPresentation.Owner!, "Delete?", $"Delete category {category.Name}?"))
            {
                _mainPresentation.DeleteCategory(category);
            }
        }
    }
}
