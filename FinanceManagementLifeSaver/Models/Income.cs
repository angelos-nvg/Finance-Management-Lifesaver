using System;

namespace FinanceManagementLifesaver.Models
{
    public class Income
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string IncomeType { get; set; }
        public DateTime Date { get; set; }
        public string Designation { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}