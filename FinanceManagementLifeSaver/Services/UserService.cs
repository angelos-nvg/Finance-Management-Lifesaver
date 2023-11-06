using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        { 
            _context = context; 
            _mapper = mapper; 
        } 

        public async Task<ServiceResponse<User>> CreateUser(UserSaveDTO _user)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User user = new User
            {
                Email = _user.Email,
                Password = _user.Password,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<User>> DeleteUser(int userId)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                response.Success = false;
                return response;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<User>>> GetAllUsers()
        {
            ServiceResponse<IEnumerable<User>> response = new ServiceResponse<IEnumerable<User>>();
            IEnumerable<User> users = await _context.Users.ToListAsync();
            response.Data = users;
            return response;
        }

        public async Task<ServiceResponse<User>> GetUserByEmailAndPassword(UserLoginDTO userLoginDTO)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDTO.Email && u.Password == userLoginDTO.Password);
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
            _context.Users.Update(_user);
            await _context.SaveChangesAsync();
            response.Data = _user;
            return response;
        }
    }
}