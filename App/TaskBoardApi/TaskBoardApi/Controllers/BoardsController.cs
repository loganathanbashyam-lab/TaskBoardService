using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskBoard.Services;

namespace TaskBoardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _service;

        public BoardsController(IBoardService service)
        {
            _service = service;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBoardsForUser(int userId)
        {
            var boards = await _service.GetBoardsForUserAsync(userId);
            return Ok(boards);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var board = await _service.CreateBoardAsync(request.Name, request.UserId);
            return CreatedAtAction(nameof(GetBoardsForUser), new { userId = request.UserId }, board);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var success = await _service.DeleteBoardAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }

    public class CreateBoardRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public int UserId { get; set; }
    }
}