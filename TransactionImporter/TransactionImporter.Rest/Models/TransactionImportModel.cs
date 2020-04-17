using System;

namespace TransactionImporter.Rest.Models
{
    public class TransactionImportModel
    {
        public string Id { get; set; }

        public decimal? Amount { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime? TransactionDate { get; set; }

        public TransactionStatus? TransactionStatus { get; set; }
    }
}