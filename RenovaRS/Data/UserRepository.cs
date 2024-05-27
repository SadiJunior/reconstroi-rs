using Microsoft.EntityFrameworkCore;
using RenovaRS.Data.Context;
using RenovaRS.Models;

namespace RenovaRS.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await dataContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesByUserIdAsync(int id)
        {
            return await dataContext
                .Services
                .Where(service => service.UserId == id)
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await dataContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await dataContext.Users.SingleOrDefaultAsync(user =>
                user.Email.Equals(
                    user.Email,
                    StringComparison.OrdinalIgnoreCase));
        }

        public async Task RegisterUserAsync(User user)
        {
            dataContext.Users.Add(user);

            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            // TODO: Check this.
            this.dataContext.Entry(user).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await dataContext.Users.FindAsync(id);

            if (user is null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            
            dataContext.Users.Remove(user);

            await dataContext.SaveChangesAsync();
        }
    }
}
