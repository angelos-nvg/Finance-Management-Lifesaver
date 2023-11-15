using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Models
{
    public class Scope
    {
        public int Id { get; set; }
        public Account AccountId { get; set; }

    }
}
