using System;

namespace TransactionImporter.Rest.Models
{
    public class TransactionCsv
    {
        public string Id { get; set; }

        public decimal? Amount { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime? TransactionDate { get; set; }

        public CsvTransactionStatus? TransactionStatus { get; set; }
    }
}
