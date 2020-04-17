using System;
using System.Collections.Generic;
using System.Linq;
using TransactionImporter.DataAccess;
using TransactionImporter.DataAccess.Entities;

namespace TransactionImporter.Services
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