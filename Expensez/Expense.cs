using System;

namespace Expensez {
    public class Expense {
        public DateTime Date { get; }
        public string Recipient { get; }
        public decimal Amount { get; }

        public Expense(DateTime date, string recipient, decimal amount) {
            Date = date;
            Recipient = recipient;
            Amount = amount;
        }
    }
}
