using FinanceManagementLifesaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface ITransactionService
    {
        Task<ServiceResponse<Transaction>> CreateTransaction(Transaction transaction);
        Task<ServiceResponse<Transaction>> GetTransactionById(int transactionId);
        Task<ServiceResponse<Transaction>> UpdateTransaction(Transaction transaction);
        Task<ServiceResponse<Transaction>> DeleteTransaction(int transactionId);
    }
}
