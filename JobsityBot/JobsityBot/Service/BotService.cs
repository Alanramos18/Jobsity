using System;
using System.Linq;
using System.Text;
using JobsityBot.Service.Interfaces;

namespace JobsityBot.Service
{
    public class BotService : IBotService
    {
        private readonly IFileReader _fileReader;
        public BotService(IFileReader fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }
        public string GetStockPrice(string code)
        {
            var list = _fileReader.ReadQuotes();

            var stock = list.FirstOrDefault(x => x.Symbol.ToLower() == code.ToLower());

            if (stock == null)
                return "Invalid code";

            StringBuilder result = new StringBuilder();
            result.Append(stock.Symbol);
            result.Append(" quote is ");
            result.Append($"${stock.Close} per share");

            return result.ToString();
        }
    }
}
