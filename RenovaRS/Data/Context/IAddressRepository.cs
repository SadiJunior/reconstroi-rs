using RenovaRS.Models;

namespace RenovaRS.Data.Context
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAddressesAsync();

        Task<Address> GetAddressAsync(int id);

        Task AddAddressAsync(Address address);

        Task UpdateAddressAsync(Address address);

        Task DeleteAddressAsync(int id);
    }
}
