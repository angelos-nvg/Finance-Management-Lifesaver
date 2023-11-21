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
using FinanceManagementLifesaver.DTO.ToDoDTO;
using FinanceManagementLifesaver.Migrations;

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

        public async Task<ServiceResponse<ToDoSaveDTO>> CreateToDo(ToDoSaveDTO toDo)
        {
            ServiceResponse<ToDoSaveDTO> response = new ServiceResponse<ToDoSaveDTO>();
                ToDo _toDo = new ToDo
                {
                     StartDate = toDo.StartDate,
                     EndDate = toDo.EndDate,
                     Description = toDo.Description,
                     UserId = toDo.UserId,
                };

            await _context.ToDos.AddAsync(_toDo);
            await _context.SaveChangesAsync();
            toDo.Id = _toDo.Id;
            response.Data = toDo;
            return response;
        }

        public Task<ServiceResponse<ToDoIDDTO>> DeleteToDos(ToDoIDDTO toDoID)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ToDoIDDTO>> GetAllToDos(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ToDo>> GetAllToDosByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ToDo>> GetToDoByFilter(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<ToDo>>> GetToDoByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ToDoSaveDTO>> UpdateAllToDos(ToDoSaveDTO todos)
        {
            throw new NotImplementedException();
        }
    }
}
