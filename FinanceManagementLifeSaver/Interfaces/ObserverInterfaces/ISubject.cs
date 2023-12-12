using System;

namespace FinanceManagementLifesaver.Interfaces.ObserverInterfaces
{
    public interface ISubject
    {
        public void Attach(IObserver subscriber);
        public void Detach(IObserver subscriber);
        public void Notify();
    }
}
