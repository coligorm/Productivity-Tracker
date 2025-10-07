using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class TaskDbContext : TaskDbContext

        // Constructor
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    // expose Tasks table for querying and saving Task entities in dbcontext
    public Dbset<Task> Tasks { get; set; }
    
    protected override void OnConfiguration(DbContextOptionsBuilder optionsBuilder)
    {
        // Disable warning for model change
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Task entity
        modelBuilder.Entity<Task>()
            .Property(testc => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Task>()
            .Property(testc => t.Category)
            .IsRequired();

        modelBuilder.Entity<Task>()
            .Property(testc => t.Score)
            .IsRequired();

    }
}