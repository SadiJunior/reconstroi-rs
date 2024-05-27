using Microsoft.EntityFrameworkCore;
using ReconstroiRS.Data;
using ReconstroiRS.Interfaces;
using ReconstroiRS.Models;

namespace ReconstroiRS.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext dataContext;

        public ServiceRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await dataContext.Services.ToListAsync();
        }

        public async Task<Service> GetServiceAsync(int id)
        {
            return await dataContext.Services.FindAsync(id);
        }

        public async Task AddServiceAsync(Service service)
        {
            dataContext.Services.Add(service);

            await dataContext.SaveChangesAsync();
        }
        public async Task UpdateServiceAsync(Service service)
        {
            // TODO: Check this.
            dataContext.Entry(service).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteServiceAsync(int id)
        {
            var service = await dataContext.Services.FindAsync(id);

            if (service is null)
            {
                throw new KeyNotFoundException($"Service with ID {id} not found.");
            }

            dataContext.Services.Remove(service);

            await dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> SearchServicesAsync(string type, string cityRange, string keyword)
        {
            var query = dataContext.Services.AsQueryable();

            bool hasTypeSearch = !string.IsNullOrEmpty(type);
            bool hasCityRangeSearch = !string.IsNullOrEmpty(cityRange);
            bool hasKeywordSearch = !string.IsNullOrEmpty(keyword);

            if (hasTypeSearch)
            {
                string typePattern = $"%{type}%";

                query = query.Where(service =>
                    service.Type.Any(t =>
                        EF.Functions.Like(t, typePattern)));
            }

            if (hasCityRangeSearch)
            {
                string cityRangePattern = $"%{cityRange}%";

                query = query.Where(service =>
                    service.CityRanges.Any(c =>
                        EF.Functions.Like(c, cityRange)));
            }

            if (hasKeywordSearch)
            {
                string keywordPattern = $"%{keyword}%";

                query = query.Where(service =>
                    EF.Functions.Like(service.Title, keywordPattern) ||
                    EF.Functions.Like(service.Description, keywordPattern));
            }

            return await query.ToListAsync();
        }
    }
}
