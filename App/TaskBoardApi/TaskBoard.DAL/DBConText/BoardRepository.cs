using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Entities;

namespace TaskBoard.Data.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly TaskBoardContext _context;

        public BoardRepository(TaskBoardContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Board>> GetBoardsByUserAsync(int userId) =>
            await _context.Boards.Where(b => b.UserId == userId).ToListAsync();

        public async Task<Board> CreateBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<bool> DeleteBoardAsync(int boardId)
        {
            var board = await _context.Boards.FindAsync(boardId);
            if (board == null) return false;
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}