using AutoMapper;
using FinanceManagementLifesaver.DTO;
using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Models;
using FinanceManagementLifesaver.ServiceResponse;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using FinanceManagementLifesaver.Validations;
using FluentValidation;
using System;
using FinanceManagementLifesaver.DTO.ToDoDTO;

namespace FinanceManagementLifesaver.Interfaces
{
    public interface IToDoService
    {
        Task<ServiceResponse<ToDoSaveDTO>> CreateToDo(ToDoSaveDTO toDo);
        Task<ServiceResponse<IEnumerable<ToDo>>> GetToDoByUserId(int userId);
        Task<ServiceResponse<ToDoIDDTO>> GetAllToDos(int id);
        Task<ServiceResponse<ToDo>> GetAllToDosByDate(DateTime date);
        Task<ServiceResponse<ToDoSaveDTO>> UpdateAllToDos(ToDoSaveDTO todos);
        Task<ServiceResponse<ToDoIDDTO>> DeleteToDos(ToDoIDDTO toDoID);
        Task<ServiceResponse<ToDo>> GetToDoByFilter(DateTime startDate, DateTime endDate);
    }
}
