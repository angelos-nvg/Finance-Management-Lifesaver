using FinanceManagementLifesaver.Interfaces.ObserverInterfaces;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagementLifesaver.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagementLifesaver.Models
{
    public class User : IObserver 
    {
        [NotMapped]
        public INotificationService _notificationService;
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ScopeId { get; set; }
        List<Notification> Notifications { get; set; }

        public void update(string message)
        {
            _notificationService.CreateNotification(message, Id);
        }
    }
}
