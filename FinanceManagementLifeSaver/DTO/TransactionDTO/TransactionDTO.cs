using FinanceManagementLifesaver.Enums;
using System;

namespace FinanceManagementLifesaver.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; } 
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int ScopeId { get; set; }
    }
}