using Microsoft.EntityFrameworkCore;
using RenovaRS.Data.Context;
using RenovaRS.Models;

namespace RenovaRS.Data
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext dataContext;

        public AddressRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync()
        {
            return await dataContext.Addresses.ToListAsync();
        }

        public async Task<Address> GetAddressAsync(int id)
        {
            return await dataContext.Addresses.FindAsync(id);
        }

        public async Task AddAddressAsync(Address address)
        {
            dataContext.Addresses.Add(address);

            await dataContext.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            // TODO: Check this.
            this.dataContext.Entry(address).State = EntityState.Modified;

            await this.dataContext.SaveChangesAsync();
        }

        public async Task DeleteAddressAsync(int id)
        {
            var address = await dataContext.Addresses.FindAsync(id);

            if (address is null)
            {
                throw new KeyNotFoundException($"Address with ID {id} not found.");
            }

            dataContext.Addresses.Remove(address);

            await dataContext.SaveChangesAsync();
        }
    }
}
