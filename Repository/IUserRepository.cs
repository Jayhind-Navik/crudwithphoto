using Assignment_Ducat.Models;

namespace Assignment_Ducat.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
    }
}
