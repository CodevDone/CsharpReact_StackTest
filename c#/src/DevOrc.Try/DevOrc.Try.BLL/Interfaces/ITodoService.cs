using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevOrc.Try.BLL.Models;

namespace DevOrc.Try.BLL.Interfaces
{
    public interface ITodoService
    {
        Task<TodoModel> CreateTodoAsync(TodoModel todoModel);
        Task<TodoModel> GetTodoAsync(int todoId);
        Task<TodoModel> UpdateTodoAsync(TodoModel todoModel);
        Task DeleteTodosAsync (int todoId);
        Task<List<TodoModel>> GetTodosAsync();
    }
}
