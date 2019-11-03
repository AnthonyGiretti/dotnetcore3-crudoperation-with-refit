using DemoRefit.Models;
using DemoRefit.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoRefit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Country>> Get()
        {
            return await _countryRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _countryRepository.GetAsync(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(Country country)
        {
            await _countryRepository.CreateAsync(country);

            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id, Country country)
        {
            await _countryRepository.ReplaceAsync(id, country);
            return NoContent();
        }

        [HttpPatch("update/{id}/description")]
        public async Task<IActionResult> Patch(int id, [FromBody]string description)
        {
            await _countryRepository.UpdateAsync(id, description);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}