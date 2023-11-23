using Microsoft.OpenApi.Writers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Models
{
    public class Scope
    {
        public int Id { get; set; }

        [ForeignKey("userId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
