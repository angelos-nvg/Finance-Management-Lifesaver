using FinanceManagementLifesaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(int accountId);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(int accountId);
    }
}
