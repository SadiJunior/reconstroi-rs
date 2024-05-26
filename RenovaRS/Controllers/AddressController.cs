using Microsoft.AspNetCore.Mvc;
using RenovaRS.Data;
using RenovaRS.Data.Context;
using RenovaRS.Models;

namespace RenovaRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return Ok(await addressRepository.GetAddressesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await addressRepository.GetAddressAsync(id);

            if (address is null)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            return Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Address>>> PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await addressRepository.AddAddressAsync(address);

            return Ok(await addressRepository.GetAddressesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Address>>> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            await addressRepository.UpdateAddressAsync(address);

            return Ok(await addressRepository.GetAddressesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Address>>> DeleteAddress(int id)
        {
            var dbAddress = await addressRepository.GetAddressAsync(id);

            if (dbAddress is null)
            {
                return NotFound($"Address with ID {id} not found.");
            }

            await addressRepository.DeleteAddressAsync(id);

            return Ok(await addressRepository.GetAddressesAsync());
        }
    }
}
