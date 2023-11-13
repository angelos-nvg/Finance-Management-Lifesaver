using System.Collections;
using System.Collections.Generic;
using FinanceManagementLifesaver.Models;
namespace FinanceManagementLifesaver.DTO
{
    public class UserSaveDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}