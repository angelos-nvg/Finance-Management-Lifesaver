using FinanceManagementLifesaver.Enums;
using System;

namespace FinanceManagementLifesaver.DTO
{
    public class TransactionDTO
    {
        public int Id;
        public decimal Amount;
        public TransactionType TransactionType;
        public DateTime Date;
        public string Description;
    }
}