using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // expose TaskModels table for querying and saving TaskModel entities in dbcontext
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            // Disable warning for model change
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TaskModel entity
            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Category)
                .IsRequired();

            modelBuilder.Entity<TaskModel>()
                .Property(t => t.Score)
                .IsRequired()
                .HasConversion<int>();

            // Add dummy data for testing purposes
            modelBuilder.Entity<TaskModel>().HasData(
                new TaskModel
                {
                    Id = 1,
                    Name = "Programming",
                    Category = Category.Mind,
                    Score = Score.High
                },
                new TaskModel
                {
                    Id = 2,
                    Name = "Play bass",
                    Category = Category.Soul,
                    Score = Score.Medium
                },
                new TaskModel
                {
                    Id = 3,
                    Name = "Read",
                    Category = Category.Soul,
                    Score = Score.Low
                },
                new TaskModel
                {
                    Id = 4,
                    Name = "Brazilian Jiu-Jitsu",
                    Category = Category.Body,
                    Score = Score.High
                }
            );
        }
    }
}