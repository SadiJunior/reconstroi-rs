using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenovaRS.Data;
using RenovaRS.Data.Context;
using RenovaRS.Models;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;

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
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await serviceRepository.AddServiceAsync(service);

            return Ok(await serviceRepository.GetServicesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            await serviceRepository.UpdateServiceAsync(service);

            return Ok(await serviceRepository.GetServicesAsync());
        }
    }
}
