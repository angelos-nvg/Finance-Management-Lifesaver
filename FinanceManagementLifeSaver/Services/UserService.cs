using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<User>> CreateUser(User user)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> DeleteUser(int userId)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<User>>> GetAllUsers()
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            IEnumerable<User> users = await _context.Users.ToListAsync();
            response.Data = users;
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserById(int userId)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> UpdateUser(User user)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            _user.Email = user.Email;
            _user.Password = user.Password;
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.Accounts = user.Accounts;
            _context.Users.Update(_user);
            await _context.SaveChangesAsync();
            response.Data = _user;
            return response;
        }
    }
}