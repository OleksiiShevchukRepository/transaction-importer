using DummyMvc.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using TransactionImporter.DataAccess.Entities;

namespace DummyMvs.Services
{
    public class TransactionReader : ITransactionReader
    {
        private readonly TransactionDbContext _dbContext;
        public TransactionReader(TransactionDbContext dbContext)
            => _dbContext = dbContext;

        public IEnumerable<Transaction> GetTransactionsByPredicate(Func<Transaction, bool> predicate)
           => _dbContext.Transactions.Where(predicate);
    }
}