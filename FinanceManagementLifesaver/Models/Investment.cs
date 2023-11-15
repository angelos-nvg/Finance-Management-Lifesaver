using FinanceManagementLifesaver.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace FinanceManagementLifesaver.Models
{
    public class Investment
    {
        public int Id{ get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InvestedMoney { get; set; }
        public decimal StartMoney { get; set; }
        public DateTime StartDate { get; set; }
        public InvestmentType InvestmentType { get; set; }
        public decimal RoI { get; set; }
        public int ScopeId { get; set; }
    }
}
