﻿using FinanceManagementLifesaver.Enums;

namespace FinanceManagementLifesaver.DTO
{
    public class TransactionSaveDTO
    {
        public decimal Amount;
        public TransactionType TransactionType;
        public string Date;
        public string Description;
    }
}