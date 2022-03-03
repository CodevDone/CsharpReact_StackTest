using System.Runtime.InteropServices;
using System;

using DevOrc.Try.DAL.Interfaces;
using Xunit;
using NSubstitute;
using DevOrc.Try.BLL.Services;

namespace DevOrc.Try.Tests.BLL
{
    public class TodoServiceTest
    {

        [Fact]
        public async void CreateTodo_Throws_ArgumentNullException_When_TodoModel_IsNull()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var todoService = new TodoService(todoRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(() => todoService.CreateTodoAsync(null));
        }

        [Fact]
        public async void GetTodo_Maps_Model_Correctly()
        {
            var todoRepository = Substitute.For<ITodoRepository>();
            var todoService = new TodoService(todoRepository);

            var entity = new DevOrc.Try.DAL.Model.Todo
            {
                TodoId = 100,
                Description = "Test todo entity",
                IsComplete = false
            };

            todoRepository.FindAsync(entity.TodoId).Returns(entity);

            var todoModel = await todoService.GetTodoAsync(entity.TodoId);

            Assert.Equal(entity.TodoId,todoModel.Id);
            Assert.Equal(entity.Description,todoModel.Description);
            Assert.Equal(entity.IsComplete,todoModel.IsCompleted);

        }

    }
}