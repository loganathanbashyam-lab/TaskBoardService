using TaskBoard.Data.Entities;

namespace TaskBoard.Services
{
    public interface IBoardService
    {
        Task<IEnumerable<Board>> GetBoardsForUserAsync(int userId);
        Task<Board> CreateBoardAsync(string name, int userId);
        Task<bool> DeleteBoardAsync(int boardId);
    }
}

