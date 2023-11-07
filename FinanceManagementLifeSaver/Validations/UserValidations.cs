using FinanceManagementLifesaver.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Validations
{
    public static class UserValidations
    {

        public static async Task<bool> CheckIfEmailTaken(DataContext _context, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user.Email != null)
            {
                return true; 
            }
            return false;
        }
    }
}
