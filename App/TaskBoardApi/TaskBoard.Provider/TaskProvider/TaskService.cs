using TaskBoard.Data.Entities;
using TaskBoard.Data.Repositories;

namespace TaskBoard.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksForBoardAsync(int boardId) =>
            await _repository.GetTasksByBoardAsync(boardId);

        public async Task<TaskItem> AddTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Task title cannot be empty");

            if (task.StartDate > task.EndDate)
                throw new ArgumentException("Start date cannot be after end date");

            return await _repository.AddTaskAsync(task);
        }

        public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Task title cannot be empty");

            if (task.StartDate > task.EndDate)
                throw new ArgumentException("Start date cannot be after end date");

            return await _repository.UpdateTaskAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(int taskId) =>
            await _repository.DeleteTaskAsync(taskId);
    }
}
