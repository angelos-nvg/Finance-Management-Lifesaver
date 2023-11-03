using FinanceManagementLifesaver.Enums;
using FinanceManagementLifesaver.Models;
using System.Collections.Generic;

namespace FinanaceManagementLifesaver.DTO
{
    public class AccountSaveDTO
    {
        public string Name { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
    }
}