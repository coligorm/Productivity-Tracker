namespace backend.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Score Score { get; set; }
    }

    public enum Category
    {
        Mind,
        Body,
        Soul
    }

    public enum Score
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}