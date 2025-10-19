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

		public async Task<TaskModel> AddTaskAsync(TaskModel newTask, int categoryId)
		{
            // Load Category Table
			var category = await _context.Categories
				.Include(c => c.Tasks)
				.FirstOrDefaultAsync(c => c.Id == categoryId);

            // Valid new task checks
            if (category == null)
            {
                throw new InvalidOperationException($"Category with ID {categoryId} not found");
            }

            if (category.Tasks.Any(t => t.Score == newTask.Score))
			{
                throw new InvalidOperationException($"There can only be 1 Task with a score of {newTask.Score} within each category: {category.Type}" +
					$"\nPlease select a different score between 1 - 3.");
            }

			if (category.Tasks.Count >= 3)
			{
                throw new InvalidOperationException($"Category: {category.Type} already has the maximum of 3 tasks." +
                    $"\nPlease select a different score between 1 - 3.");
            }

            // Set forign key, add task and save
            newTask.CategoryId = categoryId;
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
			
            return newTask;
		}

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.Include(t => t.Category).ToListAsync();
        }

        public async Task<TaskModel?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);
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