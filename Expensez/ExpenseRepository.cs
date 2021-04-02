using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Expensez {
    public class ExpenseRepository {


        public Expense[] Load() {
            var files = new DirectoryInfo(Constants.RootFolder).GetFiles("*.csv");
            return files.SelectMany(ReadCsv).OrderByDescending(e => e.Date).ToArray();
        }



        private class CsvExpense {
            public DateTime Bokföringsdatum { get; set; }
            public DateTime Transaktionsdatum { get; set; } 
            public string Transaktionstyp { get; set; }
            public string Meddelande { get; set; }
            public decimal Belopp { get; set; }

        }


        private IEnumerable<Expense> ReadCsv(FileInfo csvFile) {

            //"Kontonummer"; "Kontonamn"; ""; "Saldo"; "Tillgängligt belopp"
            //"90251561730"; "DoD kortkonto"; ""; "9 306,18"; "9 306,18"
            //
            //"Bokföringsdatum"; "Transaktionsdatum"; "Transaktionstyp"; "Meddelande"; "Belopp"
            //"2020-12-31"; "2020-12-30"; "Kortköp"; "OKQ8,LINKÖPING,SE"; "-122,00"
            //"2020-12-28"; "2020-12-28"; "Swish till 123INK AB"; "123ink se"; "-209,00"
            // ...

            using var reader = new StreamReader(csvFile.FullName);
            // skip the first 3 lines
            reader.ReadLine();
            reader.ReadLine();
            reader.ReadLine();
            using var csv = new CsvReader(reader, CultureInfo.CurrentCulture);
            var records = csv.GetRecords<CsvExpense>().ToArray();

            return records
                .Select(r => new Expense(r.Transaktionsdatum, $"{r.Transaktionstyp} {r.Meddelande}", r.Belopp))
                .Where(e => e.Amount < 0);
        }
    }
}
