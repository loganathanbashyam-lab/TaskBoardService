using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Entities;

namespace TaskBoard.Data
{
    public class TaskBoardContext : DbContext
    {
        public TaskBoardContext(DbContextOptions<TaskBoardContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
