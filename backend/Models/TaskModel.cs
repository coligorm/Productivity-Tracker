namespace backend.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ScoreLevel Score { get; set; }

        // Foreign keys to help one-to-many relationship for Category-to-Task
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }

    public enum ScoreLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}