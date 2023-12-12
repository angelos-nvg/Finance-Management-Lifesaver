using AutoMapper.Configuration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagementLifesaver.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
