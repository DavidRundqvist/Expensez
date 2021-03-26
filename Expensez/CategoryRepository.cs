using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Expensez {
    public class CategoryRepository {

        private Dictionary<string, string> _cache = new Dictionary<string, string>();

        private readonly string _fileStorage = $"{Constants.RootFolder}\\categories.json";

        public Category[] AllCategories => new[] { "Restaurang", "Livsmedel", "Bil", "Boende" }
            .Select((name, index) => new Category(name, index)).ToArray();

        internal void SetCategory(string category, params string[] recipients) {
            foreach(var recipient in recipients) {
                _cache[recipient] = category;
            }
            SaveCategories();            
        }

        private void SaveCategories() {
            var jsonData = JsonConvert.SerializeObject(_cache, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_fileStorage, jsonData);
        }

        public IDictionary<string, string> Load() {
            LoadCategories();
            return _cache;
        }

        private void LoadCategories() {
            if (File.Exists(_fileStorage)) {
                var jsonData = File.ReadAllText(_fileStorage);
                _cache = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            }
        }
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
