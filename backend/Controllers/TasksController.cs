using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;

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
		public async TaskModel<ActionResult> AddTask(TaskModel taskModel)
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
	}
}