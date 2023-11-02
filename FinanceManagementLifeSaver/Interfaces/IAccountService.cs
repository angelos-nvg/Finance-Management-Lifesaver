using FinanceManagementLifesaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<Account>> CreateAccount(Account account);
        Task<ServiceResponse<Account>> GetAccountById(int accountId);
        Task<ServiceResponse<Account>> UpdateAccount(Account account);
        Task<ServiceResponse<Account>> DeleteAccount(int accountId);
    }
}
