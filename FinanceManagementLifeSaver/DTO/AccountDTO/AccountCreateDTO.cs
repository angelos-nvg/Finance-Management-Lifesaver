using FinanceManagementLifesaver.Enums;
using FinanceManagementLifesaver.Models;
using System.Collections.Generic;

namespace FinanceManagementLifesaver.DTO
{
    public class AccountCreateDTO
    {
        public string Name { get; set; }
        public decimal AccountBalance { get; set; }
        public AccountType AccountType { get; set; }
        public int UserId { get; set; }
    }
}