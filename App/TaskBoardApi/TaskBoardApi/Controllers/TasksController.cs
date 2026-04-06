using Microsoft.AspNetCore.Mvc;
using TaskBoard.Data.Entities;
using TaskBoard.Services;

namespace TaskBoardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/tasks/board/1
        [HttpGet("board/{boardId}")]
        public async Task<IActionResult> GetTasksForBoard(int boardId)
        {
            var tasks = await _service.GetTasksForBoardAsync(boardId);
            return Ok(tasks);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _service.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTasksForBoard), new { boardId = task.BoardId }, created);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem task)
        {
            if (id != task.Id) return BadRequest("Task ID mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateTaskAsync(task);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _service.DeleteTaskAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}