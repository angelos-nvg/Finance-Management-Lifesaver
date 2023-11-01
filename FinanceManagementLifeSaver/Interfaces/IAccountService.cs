using FinanceManagementLifesaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountById(int AccountId);
        void CreateAccount(Account Account);
        void UpdateAccount(Account Account);
        void DeleteAccount(int AccountId);
    }
}
