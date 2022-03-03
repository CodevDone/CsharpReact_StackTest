using System;

namespace DevOrc.Try.DAL.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string  Description { get; set; }
        public bool IsCompleted {get; set;}

    }
}
