using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Categories table
        public DbSet<CategoryModel> Categories { get; set; }

        // Tasks table
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            // Disable warning for model change
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TaskModel / Category relationsi
            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure TaskModel properties
            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<TaskModel>()
                .Property(t => t.CategoryId)
                .IsRequired();

            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Score)
                .IsRequired()
                .HasConversion<int>();

            // Seed Existing categories
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Type = CategoryType.Mind, Description = "Mental tasks that exercise your Mind", TotalScore = 0 },
                new CategoryModel { Id = 2, Type = CategoryType.Body, Description = "Physical tasks that exercise your Body", TotalScore = 0 },
                new CategoryModel { Id = 3, Type = CategoryType.Soul, Description = "Express yourself with tasks that grow your Soul", TotalScore = 0 }
            );

            // Add dummy data for testing purposes
            modelBuilder.Entity<TaskModel>().HasData(

                // Mind Tasks (Category 1)
                new TaskModel
                {
                    Id = 1,
                    Name = "Complete Sudoku",
                    CategoryId = 1,
                    Score = ScoreLevel.Low
                },
                new TaskModel
                {
                    Id = 2,
                    Name = "Programming",
                    CategoryId = 1,
                    Score = ScoreLevel.High
                },

                // Body Tasks (Category 2)
                new TaskModel
                {
                    Id = 3,
                    Name = "Do 10 pushups",
                    CategoryId = 2,
                    Score = ScoreLevel.Low
                },
                new TaskModel
                {
                    Id = 4,
                    Name = "Run",
                    CategoryId = 2,
                    Score = ScoreLevel.Medium
                },
                new TaskModel
                {
                    Id = 5,
                    Name = "Brazilian Jiu-Jitsu",
                    CategoryId = 2,
                    Score = ScoreLevel.High
                },
                // Soul Tasks (Category 3)
                new TaskModel
                {
                    Id = 6,
                    Name = "Read",
                    CategoryId = 3,
                    Score = ScoreLevel.Low
                },
                new TaskModel
                {
                    Id = 7,
                    Name = "Play bass",
                    CategoryId = 3,
                    Score = ScoreLevel.Medium
                }
            );
        }
    }
}