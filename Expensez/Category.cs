using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace Expensez {

    public class Category {
        private Regex[] _regexes;

        public string Name { get; set; }

        public string Color { get; set; } = Colors.LightGreen.ToString();

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

        public Category(string name, string color, string[] patterns) {
            Name = name;
            Color = color;
            Patterns = patterns;
        }

        public bool MatchesRecipient(Expense expense) => _regexes.Any(r => r.IsMatch(expense.Recipient));
    }
}
