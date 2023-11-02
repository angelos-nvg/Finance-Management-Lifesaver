using FinanceManagementLifesaver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> CreateUser(User user);
        Task<ServiceResponse<User>> GetUserById(int userId);
        Task<ServiceResponse<User>> UpdateUser(User user);
        Task<ServiceResponse<User>> DeleteUser(int userId);
        Task<ServiceResponse<IEnumerable<User>>> GetAllUsers();
    }
}
