using FinanceManagementLifesaver.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FinanceManagementLifesaver.Models.BaseClass
{
    public class BaseClassInvestment
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InvestedMoney { get; set; }
        public decimal StartMoney { get; set; }
        public DateTime StartDate { get; set; }
        public InvestmentType InvestmentType { get; set; }
        public decimal RoI { get; set; }
        public Account Account { get; set; }
    }
}
