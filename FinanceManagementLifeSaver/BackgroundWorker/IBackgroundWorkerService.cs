using System.Threading.Tasks;

namespace FinanceManagementLifesaver.BackgroundWorker
{
    public interface IBackgroundWorkerService
    {
        public Task<string> ExecuteMonthlyClosing();
    }
}
