using System.Collections.Generic;
using System;
using FinanceManagementLifesaver.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceManagementLifesaver.DTO;

namespace FinanceManagementLifesaver.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AccountBalance { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public User User { get; set; }
        public Scope ScopeId { get; set; }

    }
}