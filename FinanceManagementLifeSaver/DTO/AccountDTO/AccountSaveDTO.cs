using FinanceManagementLifesaver.Enums;
using FinanceManagementLifesaver.Models;
using System.Collections.Generic;

namespace FinanceManagementLifesaver.DTO
{
    public class AccountSaveDTO
    {
        public string Name { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
    }
}