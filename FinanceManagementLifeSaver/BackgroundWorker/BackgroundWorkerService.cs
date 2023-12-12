using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.BackgroundWorker
{
    public class BackgroundWorkerService : IBackgroundWorkerService
    {
        private readonly INotificationService _notificationService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly DataContext _context;
        public BackgroundWorkerService(INotificationService notificationService, IAccountService accountService, IUserService userService, DataContext context)
        {
            _notificationService = notificationService;
            _accountService = accountService;
            _userService = userService;
            _context = context;
        }

        public async Task<string> ExecuteMonthlyClosing()
        {
            DateTime dateTime = DateTime.Now;
            if (dateTime.Day == 1) 
            {
                ServiceResponse<IEnumerable<Account>> GetAccResponse = await _accountService.GetAllAccounts();
                if (GetAccResponse.Success)
                {
                    foreach (Account acc in GetAccResponse.Data)
                    {
                        decimal amount = await _notificationService.GetMonthlyClosing(acc);
                        ServiceResponse<List<User>> GetUserResponse = await _userService.GetUsersByScopeId(acc.ScopeId);
                        foreach (User user in GetUserResponse.Data)
                        {
                            await _notificationService.CreateNotification(amount, user.Id, acc.Name, DateTime.Now.AddMonths(-1));
                        }
                    }
                }
            }
            return ".";
        }
    }
}
