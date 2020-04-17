using System;
using System.Collections.Generic;
using TransactionImporter.DataAccess.Entities;

namespace DummyMvs.Services
{
    public interface ITransactionReader
    {
        IEnumerable<Transaction> GetTransactionsByPredicate(Func<Transaction, bool> predicate);
    }
}