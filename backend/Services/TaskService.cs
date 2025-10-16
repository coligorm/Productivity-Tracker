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
            var duplicateCheck = await _context.Tasks.AnyAsync(t => t.Category == taskModel.Category && t.Score == taskModel.Score);
			if (duplicateCheck)
			{
                throw new InvalidOperationException($"There can only be 1 Task with a score of {taskModel.Score} within each category: {taskModel.Category}" +
					$"\nPlease select a different number between 1 - 3.");
            }

            var categoryCheck = await _context.Tasks.CountAsync(t => t.Category == taskModel.Category);
			if (categoryCheck >= 3)
			{
                throw new InvalidOperationException("A category can only have 3 tasks.");
            }

			var scoreCheck = await _context.Tasks.CountAsync(t => t.Score == taskModel.Score);
			if (scoreCheck < 1 && scoreCheck > 3)
			{
                throw new InvalidOperationException("A score must be 1, 2 or 3.");
            }

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

		public async Task<TaskModel> UpdateTaskAsync(TaskModel taskModel)
		{
            _context.Entry(taskModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

			return taskModel;
        }

		public async Task<TaskModel> DeleteTaskAsync(int id)
		{
            var taskModel = await _context.Tasks.FindAsync(id);

            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();

			return taskModel;
        }

    }
}