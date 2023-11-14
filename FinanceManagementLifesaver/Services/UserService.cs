﻿using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using FinanceManagementLifesaver.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

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

        public async Task<ServiceResponse<UserSaveDTO>> CreateUser(UserSaveDTO user)
        {
            ServiceResponse<UserSaveDTO> response = new ServiceResponse<UserSaveDTO>();
            User _user = new User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            //Validation
            if (await UserValidations.CheckIfEmailTaken(_context, user.Email, user.Id))
            {
                response.Success = false;
                response.Message = "Email already taken";
                return response;
            }
            UserValidations validator = new UserValidations();
            var result = validator.Validate(user, options => 
            {
                options.IncludeRuleSets("Names", "Credentials");
            });
            response.Message = ValidationResponse.GetValidatorResponse(result.IsValid, result.Errors);
            if (response.Message != "")
            {
                response.Success = false;
                return response;
            }
            await _context.Users.AddAsync(_user);
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

        public async Task<ServiceResponse<UserSaveDTO>> UpdateUser(UserSaveDTO user)
        {
            ServiceResponse<UserSaveDTO> response = new ServiceResponse<UserSaveDTO>();
            User _user = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            _user.Email = _user.Email;
            _user.Password = _user.Password;
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            if (_user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            //Validation
            if (await UserValidations.CheckIfEmailTaken(_context, user.Email, user.Id))
            {
                response.Success = false;
                response.Message = "Email already taken";
                return response;
            }
            UserValidations validator = new UserValidations();
            var result = validator.Validate(user, options =>
            {
                options.IncludeRuleSets("Names", "Credentials");
            });
            response.Message = ValidationResponse.GetValidatorResponse(result.IsValid, result.Errors);
            if (response.Message != "")
            {
                response.Success = false;
                return response;
            }

            _context.Users.Update(_user);
            await _context.SaveChangesAsync();
            response.Data = user;
            return response;
        }
    }
}