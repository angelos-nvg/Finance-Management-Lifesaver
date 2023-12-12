using FinanceManagementLifesaver.Services;
using System.Collections.Generic;

namespace FinanceManagementLifesaver.Interfaces.ObserverInterfaces
{
    public interface IObserver
    {
        public void update(string message);
    }
}
