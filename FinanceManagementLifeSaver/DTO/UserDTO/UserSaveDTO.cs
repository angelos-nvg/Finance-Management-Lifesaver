using System.Collections
using FinanceManagementLifesaver.Models
namespace FinanaceManagementLifesaver.DTO
{
    public class UserSaveDTO
    {
        public string Email;
        public string Password;
        public string FirstName;
        public string LastName;
        public <IEnumerable<Account>> Accounts;
    }
}