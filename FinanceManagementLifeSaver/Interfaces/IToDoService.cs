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
        Task<ServiceResponse<IEnumerable<ToDo>>> GetAllToDos();
        Task<ServiceResponse<ToDoSaveDTO>> UpdateToDo(ToDoSaveDTO todo);
        Task<ServiceResponse<ToDoIDDTO>> DeleteToDos(int toDoId);
        Task<ServiceResponse<IEnumerable<ToDo>>> GetToDoByUserId(int userId);
        Task<ServiceResponse<ToDo>> GetAllToDosByDate(DateTime date);
        Task<ServiceResponse<ToDo>> GetToDoByFilter(DateTime startDate, DateTime endDate);
    }
}
