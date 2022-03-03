using System.Threading.Tasks;
using DevOrc.Try.API.Controllers;
using DevOrc.Try.BLL.Interfaces;
using DevOrc.Try.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace DevOrc.Try.Tests.API
{
    public class TodoCotnrollerTest
    {
        [Fact]
        public async void GetTodo_ReturnNotFoundResult_WhenTodoIsNotFound()
        {

            var todoService = Substitute.For<ITodoService>();
            var todoServiceLogger = Substitute.For<ILogger<TodoController>>();

            var todoContorller = new TodoController(todoServiceLogger,todoService);
            
            var todoId = 1;
            todoService.GetTodoAsync(todoId).Returns(Task.FromResult<TodoModel>(null));

            var result = await todoContorller.GetTodoAsync(todoId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async void GetTodo_ReturnsOkResult_WhenTodoIsFound()
        {
            var todoService = Substitute.For<ITodoService>();
            var todoServiceLogger = Substitute.For<ILogger<TodoController>>();
            var todoContorller = new TodoController(todoServiceLogger,todoService);
           

            var todoModel = new TodoModel
            {
                Id = 1,
                Description = "test todo",
                IsCompleted = false
            };
           

           todoService
           .GetTodoAsync(todoModel.Id)
           .Returns(Task.FromResult<TodoModel>(todoModel));
           
           var result = await todoContorller.GetTodoAsync(todoModel.Id);

           Assert.IsType<OkObjectResult>(result.Result);
        }
        
    }
    
}