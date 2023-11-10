using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface ITransactionService
    {
        Task<ServiceResponse<Transaction>> CreateTransaction(Transaction transaction);
        Task<ServiceResponse<TransactionDTO>> GetTransactionById(int transactionId);
        Task<ServiceResponse<IEnumerable<Transaction>>> GetTransactionsByAccountId(int accountId);
        Task<ServiceResponse<Transaction>> UpdateTransaction(Transaction transaction);
        Task<ServiceResponse<Transaction>> DeleteTransaction(TransactionIdDTO transactionId);
    }
}
