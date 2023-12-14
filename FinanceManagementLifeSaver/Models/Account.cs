using System.Collections.Generic;
using System;
using FinanceManagementLifesaver.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Interfaces.ObserverInterfaces;
using FinanceManagementLifesaver.Services;

namespace FinanceManagementLifesaver.Models
{
    public class Account :  ISubject
    {
        public Account()
        {
            _observers = new List<IObserver>();
        }
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AccountBalance { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "tinyint")]
        public AccountType AccountType { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public int ScopeId { get; set; }

        [NotMapped]
        private List<IObserver> _observers { get; set; }

        public void Attach(IObserver subscriber)
        {
            _observers.Add(subscriber);
        }

        public void Detach(IObserver subscriber)
        {
            _observers.Remove(subscriber);
        }

        public void Notify(string message)
        {
            foreach(IObserver observer in _observers)
            {
                observer.update(message);
            }
        }
    }
}