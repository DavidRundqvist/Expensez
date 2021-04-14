using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expensez {
    public class Categorizer {
        private readonly CategoryRepository _categoryRepository;

        public Categorizer(CategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }

        public void Categorize(IEnumerable<ExpensePresentation> expenses) {
            var categories = GetAllCategories();
            foreach (var e in expenses) {
                var matchingCategory = GetMatchingCategory(categories, e.Expense);
                e.Category = matchingCategory;
            }
        }

        public IEnumerable<(Expense Expense, Category Category)> Categorize(IEnumerable<Expense> expenses) {
            var categories = GetAllCategories();
            foreach (var e in expenses) {
                var matchingCategory = GetMatchingCategory(categories, e);
                yield return (e, matchingCategory);
            }
        }

        public Category[] GetAllCategories() {
            return _categoryRepository.Load().Concat(new[] { Constants.DefaultCategory }).ToArray();
        }

        private static Category GetMatchingCategory(IEnumerable<Category> categories, Expense e) {
            return categories.First(c => c.MatchesRecipient(e));
        }

    }
}
