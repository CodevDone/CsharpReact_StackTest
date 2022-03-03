using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevOrc.Try.BLL.Models;
using DevOrc.Try.BLL.Interfaces;
using DevOrc.Try.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace DevOrc.Try.BLL.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoModel> CreateTodoAsync(TodoModel todoModel)
        {
            if (todoModel == null)
            {
                throw new ArgumentNullException();
            }

            var todoEntity = new DAL.Model.Todo
            {
                Description = todoModel.Description,
                IsComplete = todoModel.IsComplete
            };

            todoEntity = await _todoRepository.AddAsync(todoEntity);
            return new TodoModel
            {
                Id = todoEntity.TodoId,
                Description = todoEntity.Description,
                IsComplete = todoEntity.IsComplete
            };
        }
        public async Task<TodoModel> GetTodoAsync(int todoId)
        {
            var todoEntity = await _todoRepository.FindAsync(todoId);

            if (todoEntity == null)
            {
                return null;
            }

            return new TodoModel
            {
                Id = todoEntity.TodoId,
                Description = todoEntity.Description,
                IsComplete = todoEntity.IsComplete,
            };
        }
        public async Task<TodoModel> UpdateTodoAsync(TodoModel todoModel)
        {

            var todoEntity = new DAL.Model.Todo
            {
                TodoId = todoModel.Id,
                Description = todoModel.Description,
                IsComplete = todoModel.IsComplete
            };

            todoEntity = await _todoRepository.UpdateAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.TodoId,
                Description = todoEntity.Description,
                IsComplete = todoEntity.IsComplete
            };
        }
        public async Task DeleteTodosAsync(int todoId)
        {
            await _todoRepository.RemoveAsync(todoId);
        }
        public async Task<List<TodoModel>> GetTodosAsync()
        {
            IQueryable<DAL.Model.Todo> query = _todoRepository.Get();
            return await query.Select(todo => new TodoModel
            {
                Id = todo.TodoId,
                Description = todo.Description,
                IsComplete = todo.IsComplete
            })
            .ToListAsync();

        }
    }
}