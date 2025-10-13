using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;


// service deals with business rules
namespace backend.Services
{
	public class TaskService
	{
		private readonly AppDbContext _context;

		public TaskService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<TaskModel> AddTaskAsync(TaskModel taskModel)
		{
			var count = await _context.Tasks.CountAsync(t => t.Category == taskModel.Category);
			if (count >= 3)
				throw new InvalidOperationException("A category can only have 3 tasks.");

			_context.Tasks.Add(taskModel);
			await _context.SaveChangesAsync();
			return taskModel;
		}

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskModel?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

    }
}