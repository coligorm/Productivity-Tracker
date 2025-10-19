namespace backend.Models
{
    class CategoryModel
    {
        public int Id { get; set; }
        public CategoryType Type { get; set; }
        public string Description { get; set; }
        public int TotalScore { get; set; }
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }

    public enum CategoryType
    {
        Mind,
        Body,
        Soul
    }
}