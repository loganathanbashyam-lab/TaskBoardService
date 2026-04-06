using TaskBoard.Data.Entities;
using TaskBoard.Data.Repositories;

namespace TaskBoard.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _repository;

        public BoardService(IBoardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Board>> GetBoardsForUserAsync(int userId) =>
            await _repository.GetBoardsByUserAsync(userId);

        public async Task<Board> CreateBoardAsync(string name, int userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Board name cannot be empty");

            var board = new Board { Name = name, UserId = userId };
            return await _repository.CreateBoardAsync(board);
        }

        public async Task<bool> DeleteBoardAsync(int boardId) =>
            await _repository.DeleteBoardAsync(boardId);
    }
}

