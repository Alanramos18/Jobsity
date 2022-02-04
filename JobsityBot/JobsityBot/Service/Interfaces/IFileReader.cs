using System.Collections.Generic;
using JobsityBot.CVS;

namespace JobsityBot.Service.Interfaces
{
    public interface IFileReader
    {
        List<QuotesCSV> ReadQuotes();
    }
}
