using aspnet_core_mongo_api.Models;
using aspnet_core_mongo_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_mongo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DriverService _driverService;

        public DriversController(DriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet(template: "{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var driver = await _driverService.GetAsync(id);

            if (driver is null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var driversList = await _driverService.GetAsync();

            if (driversList.Any())
            {
                return Ok(driversList);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Driver driver)
        {
            await _driverService.CreateAsync(driver);

            return CreatedAtAction(nameof(Get), new { id = driver.Id, driver });
        }

        [HttpPut(template: "{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Driver driver)
        {
            var existringDriver = await _driverService.GetAsync(id);

            if (existringDriver is null)
            {
                return BadRequest();
            }

            driver.Id = existringDriver.Id;
            await _driverService.UpdateAsync(driver);

            return NoContent();
        }

        [HttpDelete(template: "{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var driver = await _driverService.GetAsync(id);

            if (driver is null)
            {
                return BadRequest();
            }

            await _driverService.DeleteAsync(id);

            return NoContent();
        }
    }
}
