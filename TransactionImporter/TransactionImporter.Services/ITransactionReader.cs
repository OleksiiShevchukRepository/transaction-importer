using System;
using System.Collections.Generic;
using TransactionImporter.DataAccess.Entities;

namespace TransactionImporter.Services
{
    public interface ITransactionReader
    {
        IEnumerable<Transaction> GetTransactionsByPredicate(Func<Transaction, bool> predicate);
    }
}