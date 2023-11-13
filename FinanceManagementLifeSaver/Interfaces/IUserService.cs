using FinanceManagementLifesaver.DTO;
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
        Task<ServiceResponse<UserSaveDTO>> CreateUser(UserSaveDTO user);
        Task<ServiceResponse<User>> GetUserByEmailAndPassword(UserLoginDTO userLoginDTO);
        Task<ServiceResponse<UserSaveDTO>> UpdateUser(UserSaveDTO user);
        Task<ServiceResponse<User>> DeleteUser(int userId);
        Task<ServiceResponse<IEnumerable<User>>> GetAllUsers();
    }
}
