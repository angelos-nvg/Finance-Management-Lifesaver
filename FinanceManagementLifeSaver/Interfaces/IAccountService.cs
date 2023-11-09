using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<Account>> CreateAccount(AccountSaveDTO account);
        Task<ServiceResponse<AccountDTO>> GetAccountById(int accountId);
        Task<ServiceResponse<IEnumerable<Account>>> GetAccountsByUserId(int userId);
        Task<ServiceResponse<Account>> UpdateAccount(Account account);
        Task<ServiceResponse<Account>> DeleteAccount(int accountId);
    }
}
