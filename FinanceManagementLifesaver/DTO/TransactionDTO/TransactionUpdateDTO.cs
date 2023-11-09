using FinanceManagementLifesaver.Enums;
using System;

namespace FinanceManagementLifesaver.DTO
{
    public class TransactionUpdateDTO
    {
            public int Id { get; set; }
            public int Amount { get; set; }
            public TransactionType TransactionType { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; }
    }
}
