using AutoMapper.Configuration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceManagementLifesaver.Models
{
    public class Notification
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
