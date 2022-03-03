using System;
using Microsoft.EntityFrameworkCore;


namespace DevOrc.Try.DAL
{
    public class TodoContext : DbContext
    {
        public TodoContext( DbContextOptions  <TodoContext> option)
        :base(option)
        {
            
        }

        public DbSet<Entities.Todo> Todos { get; set; }

        
    }
}
