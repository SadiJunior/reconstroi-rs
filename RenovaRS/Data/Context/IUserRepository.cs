using RenovaRS.Models;

namespace RenovaRS.Data.Context
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserAsync(int id);

        Task<User> GetUserByEmailAsync(string email);

        Task RegisterUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);
    }
}
