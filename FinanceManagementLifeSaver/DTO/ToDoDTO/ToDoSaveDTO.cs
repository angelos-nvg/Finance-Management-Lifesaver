using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FinanceManagementLifesaver.DTO.ToDoDTO
{
    public class ToDoSaveDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
    }
}
