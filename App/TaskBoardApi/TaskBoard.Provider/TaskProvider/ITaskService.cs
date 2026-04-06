using TaskBoard.Data.Entities;
using TaskBoard.Data.Repositories;

namespace TaskBoard.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetTasksForBoardAsync(int boardId);
        Task<TaskItem> AddTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(TaskItem task);
        Task<bool> DeleteTaskAsync(int taskId);
    }

}