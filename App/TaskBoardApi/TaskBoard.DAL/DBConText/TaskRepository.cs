using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Entities;

namespace TaskBoard.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskBoardContext _context;

        public TaskRepository(TaskBoardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByBoardAsync(int boardId)
        {
            return await _context.Tasks
                .Where(t => t.BoardId == boardId)
                .ToListAsync();
        }

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
        {
            var existing = await _context.Tasks.FindAsync(task.Id);
            if (existing == null) return null;

            existing.Title = task.Title;
            existing.StartDate = task.StartDate;
            existing.EndDate = task.EndDate;
            existing.AssignedTo = task.AssignedTo;
            existing.Owner = task.Owner;
            existing.Priority = task.Priority;
            existing.Status = task.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
