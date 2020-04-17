using System.Text;

namespace TransactionImporter.Rest.Formatters
{
    public class CsvFormatterOptions
    {
        public bool UseSingleLineHeaderInCsv { get; set; } = false;

        public string CsvDelimiter { get; set; } = ",";

        public Encoding Encoding { get; set; } = Encoding.Default;
    }
}
