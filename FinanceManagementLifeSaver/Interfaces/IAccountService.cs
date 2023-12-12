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
        Task<ServiceResponse<AccountSaveDTO>> CreateAccount(AccountSaveDTO account);
        Task<ServiceResponse<AccountDTO>> GetAccountById(AccountIdDTO accountId);
        Task<ServiceResponse<IEnumerable<Account>>> GetAccountsByUserId(UserIdDTO userId);
        Task<ServiceResponse<IEnumerable<Account>>> GetAllAccounts();
        Task<ServiceResponse<AccountSaveDTO>> UpdateAccount(AccountSaveDTO account);
        Task<ServiceResponse<Account>> DeleteAccount(AccountIdDTO accountId);
    }
}
