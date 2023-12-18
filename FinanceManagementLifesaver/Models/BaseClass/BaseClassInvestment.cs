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
        [Column(TypeName = "decimal(18, 2)")]
        public decimal StartMoney { get; set; }
        public DateTime StartDate { get; set; }
        [Column(TypeName = "tinyint")]
        public InvestmentType InvestmentType { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal GrossIncome { get; set; }
        public Account Account { get; set; }
        public string Description { get; set; }
        [NotMapped]
        private decimal _roI;

        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RoI
        {
            get { return Math.Round(_roI); }
            set { _roI = (GrossIncome - InvestedMoney) / InvestedMoney; }
        }
    }
}
