using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DevOrc.Try.BLL;
using Microsoft.AspNetCore.Http;
using DevOrc.Try.BLL.Models;
using DevOrc.Try.API.Models;
using DevOrc.Try.BLL.Interfaces;

namespace DevOrc.Try.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;

        }

        [HttpGet("{id}")]
        [ActionName("GetTodoAsync")]
        // [Route("[controller]/GetTodoAsync/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoModel>> GetTodoAsync(int id)
        {
            var todo = await _todoService.GetTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpGet]
        [ActionName("GetTodosAsync")]
        // [Route("[controller]/GetTodosAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<TodoModel>>> GetTodosAsync()
        {
            var todos = await _todoService.GetTodosAsync();
            if (todos == null)
            {
                return NotFound();
            }

            return Ok(todos);
        }

        [HttpPost]
        [ActionName("CreateTodoAsync")]
        // [Route("[controller]/CreateTodoAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoModel>> CreateTodoAsync(CreateTodoModel createTodoModel)
        {
            var todoModel = new TodoModel
            {
                Description = createTodoModel.Description,
                IsComplete = createTodoModel.IsComplete
            };

            var createdTodo = await _todoService.CreateTodoAsync(todoModel);

            return CreatedAtAction(nameof(GetTodoAsync), new { id = createdTodo.Id }, createdTodo);
        }


        [HttpPut("{id}")]
        [ActionName("UpdateTodoAsync")]
        // [Route("[controller]/UpdateTodoAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoModel>> UpdateTodoAsync(int id, UpdateTodoModel updatoTodoModel)
        {
            if (id != updatoTodoModel.Id)
            {
                return BadRequest();
            }

            var todo = await _todoService.GetTodoAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            var todoModel = new TodoModel
            {
                Id = id,
                Description = updatoTodoModel.Description,
                IsComplete = updatoTodoModel.IsComplete,
            };

            var updateTodo = await _todoService.UpdateTodoAsync(todoModel);

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ActionName("DeleteTodoAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoModel>> DeleteTodoAsync(int id)
        {
            var todo = await _todoService.GetTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            await _todoService.DeleteTodosAsync(id);
            return NoContent();

        }
    }
}
