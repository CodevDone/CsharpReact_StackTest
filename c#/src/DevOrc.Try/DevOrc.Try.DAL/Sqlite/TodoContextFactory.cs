using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace DevOrc.Try.DAL
{
    public class TodoContextFactory: IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
          var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
          optionsBuilder.UseSqlite("Data Source=todos.db");

          return new TodoContext(optionsBuilder.Options);   
        }

        
    }
}
