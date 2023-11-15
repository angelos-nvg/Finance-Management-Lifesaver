using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceManagementLifesaver.DTO.AccountDTO;
using FinanceManagementLifesaver.Validations;

namespace FinanceManagementLifesaver.Services
{
    public class ToDoService : IToDoService 
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ToDoService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
