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
        Task<ServiceResponse<TransactionSaveDTO>> CreateTransaction(TransactionSaveDTO transaction);
        Task<ServiceResponse<TransactionDTO>> GetTransactionById(int transactionId);
        Task<ServiceResponse<IEnumerable<TransactionDTO>>> GetTransactionsByAccountId(int accountId);
        Task<ServiceResponse<TransactionDTO>> UpdateTransaction(TransactionDTO transaction);
        Task<ServiceResponse<Transaction>> DeleteTransaction(int transactionId);
    }
}
