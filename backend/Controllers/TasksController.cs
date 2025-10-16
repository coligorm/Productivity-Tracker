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
				var createdTask = await _taskService.AddTaskAsync(taskModel);
				return return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the task");
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
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch
            {
                return NotFound();
            }   
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
            return NoContent();
        }

        // DELETE: api/Tasks/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (deletedTask == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}