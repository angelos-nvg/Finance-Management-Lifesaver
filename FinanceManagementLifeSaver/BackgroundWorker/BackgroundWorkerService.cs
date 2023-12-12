using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.BackgroundWorker
{
    public class BackgroundWorkerService : IBackgroundWorkerService
    {
        private readonly INotificationService _notificationService;
        private readonly IAccountService _accountService;
        private readonly DataContext _context;
        public BackgroundWorkerService(INotificationService notificationService, IAccountService accountService, DataContext context)
        {
            _notificationService = notificationService;
            _accountService = accountService;
            _context = context;
        }

        public async Task<string> ExecuteMonthlyClosing()
        {
            DateTime dateTime = DateTime.Now;
            if (dateTime.Day == 1) 
            {
                ServiceResponse<IEnumerable<Account>> response = await _accountService.GetAllAccounts();
                if (response.Success)
                {
                    foreach (Account acc in response.Data)
                    {
                        decimal amount = await _notificationService.GetMonthlyClosing(acc);
                        await _notificationService.CreateNotification(amount, acc.User.Id, acc.Name, DateTime.Now.AddMonths(-1));
                    }
                }
            }
            return "moin";
        }
    }
}
