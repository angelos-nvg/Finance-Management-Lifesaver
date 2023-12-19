using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Models;
using System;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface INotificationService
    {
        public Task<decimal> GetMonthlyClosing(AccountDTO account);
        public Task<string> CreateMonthlyClosingNotification(decimal closing, int userId, string accountName, DateTime month);
        public Task<string> CreateNotification(string message, int userId);
        public Task<string> CheckIfAccountBalanceNegative(Account account);
    }
}
