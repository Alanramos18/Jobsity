using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using JobsityBot.CVS;
using JobsityBot.Service.Interfaces;

namespace JobsityBot.Service
{
    public class FileReader : IFileReader
    {
        public List<QuotesCSV> ReadQuotes()
        {
            using (var reader = new StreamReader(@"CVS/aapl.us.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<QuotesCSV>().ToList();
            }
        }
    }
}
