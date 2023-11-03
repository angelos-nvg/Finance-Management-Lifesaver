﻿using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManagementLifesaver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{email}/{password}")]
        public IActionResult Get(string email, string password)
        {
            var user = _userService.GetUserByEmailAndPassword(name, password);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}