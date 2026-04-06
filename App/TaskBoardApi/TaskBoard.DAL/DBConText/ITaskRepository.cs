using TaskBoard.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskBoard.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetTasksByBoardAsync(int boardId);
        Task<TaskItem> AddTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(TaskItem task);
        Task<bool> DeleteTaskAsync(int taskId);
    }
}