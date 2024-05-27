using ReconstroiRS.Models;

namespace ReconstroiRS.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServicesAsync();

        Task<Service> GetServiceAsync(int id);

        Task AddServiceAsync(Service service);

        Task UpdateServiceAsync(Service service);

        Task DeleteServiceAsync(int id);

        Task<IEnumerable<Service>> SearchServicesAsync(string type, string cityRange, string keyword);
    }
}
