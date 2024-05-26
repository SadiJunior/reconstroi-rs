using Microsoft.AspNetCore.Mvc;
using RenovaRS.Data.Context;
using RenovaRS.Models;


namespace RenovaRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices(
            [FromQuery] string type = null,
            [FromQuery] string cityRange = null,
            [FromQuery] string keyword = null)
        {
            bool hasSearch =
                !(string.IsNullOrEmpty(type) &&
                string.IsNullOrEmpty(cityRange) &&
                string.IsNullOrEmpty(keyword));

            var services = hasSearch ?
                await serviceRepository.SearchServicesAsync(type, cityRange, keyword) :
                await serviceRepository.GetServicesAsync();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await this.serviceRepository.GetServiceAsync(id);

            if (service is null)
            {
                return NotFound($"Service with ID {id} not found.");
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Service>>> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await serviceRepository.AddServiceAsync(service);

            return Ok(await serviceRepository.GetServicesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Service>>> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            await serviceRepository.UpdateServiceAsync(service);

            return Ok(await serviceRepository.GetServicesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Service>>> DeleteService(int id)
        {
            var dbService = await serviceRepository.GetServiceAsync(id);

            if (dbService is null)
            {
                return NotFound($"Service with ID {id} not found.");
            }

            await serviceRepository.DeleteServiceAsync(id);

            return Ok(await serviceRepository.GetServicesAsync());
        }
    }
}
