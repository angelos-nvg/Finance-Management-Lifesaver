using System.Collections.Generic;
using System;

namespace FinancePlan.Models
{
    public class Account
    {
        public int Id { get; set; }
        public decimal AccountBalance { get; set; }
        public string AccountType { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Expenditures> Expenditures { get; set; }
        public ICollection<Income> Income { get; set; }
    }
}