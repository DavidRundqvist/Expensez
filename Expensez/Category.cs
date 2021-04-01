using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Expensez {

    public class Category {
        private Regex[] _regexes;

        public string Name { get; set; }

        public string[] Patterns {
            get => _regexes.Select(r => r.ToString()).ToArray();
            set => _regexes = value.Select(v => new Regex(v)).ToArray();
        }

        public override string ToString() {
            return $"{Name}";
        }

        public Category() {
            Name = "";
            _regexes = Array.Empty<Regex>();
        }

        public Category(string name, string[] patterns) {
            Name = name;
            Patterns = patterns;
        }

        public bool IsMatch(Expense expense) => _regexes.Any(r => r.IsMatch(expense.Recipient));
    }
}
