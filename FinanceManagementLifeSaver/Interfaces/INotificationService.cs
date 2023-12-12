using FinanceManagementLifesaver.Models;
using System;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface INotificationService
    {
        public Task<decimal> GetMonthlyClosing(Account account);
        public Task<string> CreateNotification(decimal closing, int userId, string accountName, DateTime month);
    }
}
