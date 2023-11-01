using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePlan.Models
{
    public class Expenditures
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ExpenditureType { get; set; }
        public DateTime Date { get; set; }
        public string Designation { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
