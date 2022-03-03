using DevOrc.Try.DAL.Model;
using DevOrc.Try.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Xunit;

namespace DevOrc.Try.Tests.DAL
{
    public class TodoRepositoryTest
    {
        private DbContextOptions<todoContext> _contextOpions;
        public TodoRepositoryTest()
        {
            _contextOpions = new DbContextOptionsBuilder<todoContext>().UseInMemoryDatabase("TodoDb").Options;
        }

        [Fact]
        public async void Can_Add_Todo()
        {
            // Given
            var testTodo = new DevOrc.Try.DAL.Model.Todo
            {
                Description = "test add",
                IsComplete = false
            };
            
        
            // When
            using( var context = new todoContext(_contextOpions))
            {
                var repository = new TodoRepository(context);
                var todo = await repository.AddAsync(testTodo);
                testTodo.TodoId = todo.TodoId;
            }

            // Then
             using( var context = new todoContext(_contextOpions))
            {
                 var todo = await context.Todo.FindAsync(testTodo.TodoId);

                 //Assert.NotEqual(testTodo.TodoId,todo.TodoId);
                 Assert.Equal(testTodo.TodoId,todo.TodoId);
                 Assert.Equal(testTodo.Description,todo.Description);
                 Assert.Equal(testTodo.IsComplete,todo.IsComplete);
            }
        }



        [Fact]
        public async void Can_Update_Todo()
        {
            // Given
            var _testTodo = new DevOrc.Try.DAL.Model.Todo
            {
                Description = "update add",
                IsComplete = false
            };

            DevOrc.Try.DAL.Model.Todo _todoUpdated;
            
        
            // When
            using( var context = new todoContext(_contextOpions))
            {
                var repository = new TodoRepository(context);
                var todo = await repository.AddAsync(_testTodo);
                _testTodo.TodoId = todo.TodoId;
            }

            using( var context = new todoContext(_contextOpions))
            {
                var repository = new TodoRepository(context);
                var todoUpdated = await context.Todo.FindAsync(_testTodo.TodoId);
                todoUpdated.Description = "updated";
                todoUpdated.IsComplete = true;
                 _todoUpdated = await repository.UpdateAsync(todoUpdated);
                
            }

            // Then
             using( var context = new todoContext(_contextOpions))
            {
                 var todo = await context.Todo.FindAsync(_testTodo.TodoId);
                 Assert.Equal(_todoUpdated.TodoId,todo.TodoId);
                 Assert.Equal(_todoUpdated.Description,todo.Description);
                 Assert.Equal(_todoUpdated.IsComplete,todo.IsComplete);
            }
        }

    }
    
}