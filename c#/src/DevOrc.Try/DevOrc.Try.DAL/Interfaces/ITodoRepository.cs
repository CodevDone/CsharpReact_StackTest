using System;
using System.Threading.Tasks;
using System.Linq;

namespace DevOrc.Try.DAL.Interfaces
{
    public interface ITodoRepository
    {
        Task<Model.Todo> FindAsync(int id);
        Task<Model.Todo> UpdateAsync(Model.Todo todo);
        Task<Model.Todo> AddAsync(Model.Todo todo);
        Task RemoveAsync (int id);
        IQueryable<Model.Todo> Get();

    }
}
