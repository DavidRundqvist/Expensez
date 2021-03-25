using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expensez {
    public class CategoryRepository {

        public Category[] AllCategories => new[] { "Restaurang", "Livsmedel", "Bil", "Boende" }
            .Select((name, index) => new Category(name, index)).ToArray();

        //public (
        //    RecipientCategory[] recipientCategories, 
        //    ExpenseCategory[] expenseCategories,
        //    string[] allCategories) Load() {

        //}
    }

    public record RecipientCategory(string Recipient, string Category);

    public record ExpenseCategory(string Recipient, string Date, string Category);





    public class Category {
        public string Name { get; }
        public int Index { get; }

        public override string ToString() {
            return $"{Index}. {Name}";
        }

        public Category(string name, int index) {
            Name = name;
            Index = index;
        }
    }
}
