using TaskBoard.Data.Entities;

namespace TaskBoard.Data.Repositories
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> GetBoardsByUserAsync(int userId);
        Task<Board> CreateBoardAsync(Board board);
        Task<bool> DeleteBoardAsync(int boardId);
    }
}