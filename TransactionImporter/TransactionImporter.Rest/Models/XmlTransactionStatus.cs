using System;

namespace TransactionImporter.Rest.Models
{
    [Serializable]
    public enum XmlTransactionStatus
    {
        Approved = 1,
        Rejected,
        Done
    }
}