using FinanceManagementLifesaver.Models;
using System;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface INotificationService
    {
        public Task<decimal> GetMonthlyClosing(Account account);
        public Task<string> CreateMonthlyClosingNotification(decimal closing, int userId, string accountName, DateTime month);
        public Task<string> CheckIfAccountBalanceNegative(Account account);
    }
}
