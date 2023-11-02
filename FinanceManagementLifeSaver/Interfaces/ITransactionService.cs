using FinanceManagementLifesaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionById(int transactionId);
        Task<Transaction> GetTransactionByAccountId(int accountId);
        void CreateTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int transactionId);
    }
}
