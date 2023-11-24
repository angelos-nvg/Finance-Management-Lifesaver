using System;

namespace FinanceManagementLifesaver.BackgroundWorker
{
    public static class BackgroundWorkerService
    {
        public static void ExecuteMonthlyClosing()
        {
            Console.WriteLine("Background Worker executed");
        }
    }
}
