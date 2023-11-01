using System.Collections.Generic;
using System;
using FinancePlan.Enums;

namespace FinancePlan.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Expenditures { get; set; }
        public ICollection<Income> Income { get; set; }
    }
}