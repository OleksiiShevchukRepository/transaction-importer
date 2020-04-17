using System;

namespace TransactionImporter.Rest.Models
{
    [Serializable]
    public enum CsvTransactionStatus
    {
        Approved = 1,
        Failed,
        Finished
    }
}