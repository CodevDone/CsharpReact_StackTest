using System;
using System.Threading.Tasks;
using System.Linq;
using DevOrc.Try.DAL.Interfaces;
using DevOrc.Try.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DevOrc.Try.DAL.Repositories
{

    public class TodoRepository : ITodoRepository
    {
        private readonly todoContext _todoContext;
        

        public TodoRepository (todoContext todoContext)
        {
            _todoContext = todoContext;
           
        }

        // public async Task<Todo> FindAsync(int id)
        // {
        //     // _todoContext.Todo.FindAsync(id)
            
        //     return await  _todoContext.Todo.FindAsync(id);
        // }
        public async Task<Todo> FindAsync(int id)
        {
            // _todoContext.Todo.FindAsync(id)
            
            return  await  _todoContext.Todo.Where(x => x.TodoId == id).SingleAsync();
        }
        public async Task<Todo> UpdateAsync(Todo todo)
        {
            //var local = _todoContext.Todo.Local.FirstOrDefault(entity => entity.TodoId == todo.TodoId);
            var local =  _todoContext.Todo.Where(x => x.TodoId == todo.TodoId).SingleOrDefault();
            
                if (local != null)
                {
                    local.IsComplete = todo.IsComplete;
                    local.Description = todo.Description;
                    
                    _todoContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                }
                _todoContext.Entry(todo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _todoContext.SaveChangesAsync();
                return todo;

            // using (var db = new todoContext())
            // {
            //     var todofind = db.Todo.Where(x=>x.TodoId == todo.TodoId).FirstOrDefault();
            //     todofind.Description = todo.Description;
            //     todofind.IsComplete = todo.IsComplete;

            //     db.Entry(todofind).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //     db.SaveChanges();
            //     await Task.Delay(2000);

            //    return todofind;  
            // }

        }
        public async Task<Todo> AddAsync(Todo todo)
        {
            //todo.TodoId = todo.TodoId <= 1 ? todo.TodoId+1 : todo.TodoId;
            _todoContext.Add(todo);
            await _todoContext.SaveChangesAsync();
            return todo;
             
        }
        public async Task RemoveAsync (int id)
        {
            // var todo = await _todoContext.Todo.FindAsync(id);
            var todo = await  _todoContext.Todo.Where(x => x.TodoId == id).SingleAsync();
            if (todo != null)
            {
                _todoContext.Todo.Remove(todo);
                await _todoContext.SaveChangesAsync();
            }
        }
       

        public IQueryable<Todo> Get()
        {
            return _todoContext.Todo.AsQueryable();
        }
  
    }
}
