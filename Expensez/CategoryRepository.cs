using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Expensez {
    public class CategoryRepository {

        private List<Category> _cache = new();

        private readonly string _fileStorage = $"{Constants.RootFolder}\\categories.json";

        internal void Add(Category category) {
            _cache.Add(category);
            Save();            
        }

        public Category[] Load() {
            if (!_cache.Any()) {
                LoadCategories();
            }
            return _cache.ToArray();
        }

        private void LoadCategories() {
            if (File.Exists(_fileStorage)) {
                var jsonData = File.ReadAllText(_fileStorage);
                _cache = JsonSerializer.Deserialize<List<Category>>(jsonData) ?? [];
            }
        }

        internal void Save() {
            var jsonData = JsonSerializer.Serialize(_cache, new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(_fileStorage, jsonData);
        }

        internal void Delete(Category category) {
            _cache.Remove(category);
            Save();
        }
    }

    public record RecipientCategory(string Recipient, string Category);

    public record ExpenseCategory(string Recipient, string Date, string Category);
}
