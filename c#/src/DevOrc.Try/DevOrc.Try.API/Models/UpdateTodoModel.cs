using System;

namespace DevOrc.Try.API.Models
{

    public class UpdateTodoModel
    {
        public int Id { get; set; }
        public string Description {get ; set ;}
        public bool IsComplete { get; set; }
    }


}