using FinanaceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> CreateUser(User user);
        Task<ServiceResponse<User>> GetUserByEmailAndPassword(UserLoginDTO userLoginDTO);
        Task<ServiceResponse<User>> UpdateUser(User user);
        Task<ServiceResponse<User>> DeleteUser(int userId);
        Task<ServiceResponse<IEnumerable<User>>> GetAllUsers();
    }
}
