using System.Collections.Generic;
using System;
using FinanceManagementLifesaver.Enums;

namespace FinanceManagementLifesaver.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}