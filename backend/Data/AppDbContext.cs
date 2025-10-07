using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext

        // Constructor
        public AppDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

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
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Task>()
            .Property(t => t.Category)
            .IsRequired();

        modelBuilder.Entity<Task>()
            .Property(t => t.Score)
            .IsRequired()
            .HasConversion<int>();

        // Add dummy data for testing purposes
        modelBuilder.Entity<AppDbContext>().HasData(
            new Task
            {
                Name = "Programming",
                Category = Category.Mind,
                Score = Score.High
            },
            new Task
            {
                Name = "Play bass",
                Category = Category.Soul,
                Score = Score.Medium
            },
            new Task
            {
                Name = "Read",
                Category = Category.Soul,
                Score = Score.Low
            },
            new Task
            {
                Name = "Brazilian Jiu-Jitsu",
                Category = Category.Body,
                Score = Score.High
            }
        );

    }
}