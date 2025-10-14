using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;
using backend.Models;

// controller deals with web
namespace backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly TaskService _taskService;

		public TasksController(TaskService taskService)
		{
			_taskService = taskService;
		}

		// POST
		[HttpPost]
		public async Task<ActionResult> AddTask(TaskModel taskModel)
		{
			try
			{
				var result = await _taskService.AddTaskAsync(taskModel);
				return Ok(result);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/Tasks/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

		// PUT
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTask(int id, TaskModel taskModel)
		{
			if (id != taskModel.Id)
			{
				return BadRequest();
			}

			var updatedTask = await _taskService.UpdateTaskAsync(taskModel);
            if (updatedTask == null)
            {
                return NotFound();
            }

			return Ok(updatedTask);
        }

        // DELETE: api/Tasks/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deletedTask = await _taskService.DeleteTaskAsync(id);
            if (deletedTask == null)
            {
                return NotFound();
            }
            
            return Ok(deletedTask);
        }


    }
}