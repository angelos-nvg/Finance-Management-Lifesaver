using System.Collections.Generic;
using System;
using FinanceManagementLifesaver.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using FinanceManagementLifesaver.DTO;
using System.Text.Json.Serialization;

namespace FinanceManagementLifesaver.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AccountBalance { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "tinyint")]
        public AccountType AccountType { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public int ScopeId { get; set; }
    }
}