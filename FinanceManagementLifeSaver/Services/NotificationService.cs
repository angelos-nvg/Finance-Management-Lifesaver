using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Interfaces.ObserverInterfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Services
{
    public class NotificationService : INotificationService
    {
        public readonly DataContext _context;
        private readonly IUserService _userService;
        public NotificationService(DataContext dbContext, IUserService userService)
        {
            _context = dbContext;
            _userService = userService;
        }
        public async Task<string> CreateMonthlyClosingNotification(decimal closing, int userId, string accountName, DateTime month)
        {
            string message = "Your monthly closing amounts " + closing + " in " + month.ToString("MMMM") + " for the Account " + accountName;
            Notification notification = new Notification
            {
                Message = message,
                UserId = userId
            };
            await _context.AddAsync(notification);
            await _context.SaveChangesAsync();
            return "moin";
        }
        public async Task<string> CreateNegativeAccountBalanceNotification(decimal accountBalance, int userId, string accountName)
        {
            string message = "Your Account \"" + accountName + "\" has a negative Account Balance of " + accountBalance;
            Notification notification = new Notification
            {
                Message = message,
                UserId = userId
            };
            await _context.AddAsync(notification);
            await _context.SaveChangesAsync();
            return "moin";
        }
   
        public async Task<decimal> GetMonthlyClosing(Account account)
        {
            //first day of last month
            DateTime startDate = DateTime.Now.AddMonths(-1);
            startDate = startDate.AddDays(-startDate.Day + 1);
            startDate = startDate.AddHours(-startDate.Hour);
            startDate = startDate.AddMinutes(-startDate.Minute);
            startDate = startDate.AddSeconds(-startDate.Second);
            //last day of last month
            DateTime endDate = DateTime.Now.AddMonths(-1);
            endDate = endDate.AddDays(-endDate.Day + DateTime.DaysInMonth(endDate.Year, endDate.Month));
            endDate = endDate.AddHours(-endDate.Hour).AddHours(23);
            endDate = endDate.AddMinutes(-endDate.Minute).AddMinutes(59);
            endDate = endDate.AddSeconds(-endDate.Second).AddSeconds(59);
            //Get all transactions of last month
            List<Transaction> transactions = await _context.Transactions.Where(
                t=> t.Account.Id == account.Id
                && t.Date.Month == startDate.Month
                && t.Date.Day >= startDate.Day
                && t.Date.Day <= endDate.Day).ToListAsync();
            decimal closing = 0m;
            foreach (Transaction transaction in transactions)
            {
                closing += transaction.Amount;
            }
            return closing;
        }
        public async Task<string> CheckIfAccountBalanceNegative(Account account)
        {
            if (account.AccountBalance < 0m)
            {
                ServiceResponse<List<User>> GetUserResponse = await _userService.GetUsersByScopeId(account.ScopeId);
                foreach (User user in GetUserResponse.Data)
                {
                    await CreateNegativeAccountBalanceNotification(account.AccountBalance, user.Id, account.Name);
                }
            }
            return "moin";
        }
    }
    public class SubjectBase
    {
        public List<IObserver> _observers { get; set; }
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }
        public void Notify(string message)
        {
            foreach (IObserver observer in _observers)
            {
                observer.update(message);
            }
        }
    }
}