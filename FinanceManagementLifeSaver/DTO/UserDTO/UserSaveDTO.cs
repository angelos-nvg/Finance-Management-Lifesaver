using System.Collections;
using System.Collections.Generic;
using FinanceManagementLifesaver.Models;
namespace FinanaceManagementLifesaver.DTO
{
    public class UserSaveDTO
    {
        public string Email;
        public string Password;
        public string FirstName;
        public string LastName;
        public ICollection<Account> Accounts;
    }
}