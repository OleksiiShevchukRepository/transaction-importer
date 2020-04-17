using System;

namespace TransactionImporter.DataAccess.Entities
{
    public class Transaction
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionStatusEnum TransactionStatus { get; set; }
    }
}