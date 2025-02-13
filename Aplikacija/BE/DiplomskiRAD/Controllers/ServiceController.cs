using DiplomskiRAD.Models;
using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.OrderDTO;
using static DiplomskiRAD.DTOs.PriceListDTO;
using static DiplomskiRAD.DTOs.ServiceDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly ServiceService _serviceService;

        public ServiceController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllServicesForUser(Guid userId)
        {
            var services = await _serviceService.GetAllServicesForUser(userId);
            return Ok(services);
        }


        [HttpGet("pending-inProgress")]
        public async Task<IActionResult> GetAllPendingAndInprogressServices()
        {

            var services = await _serviceService.GetAllPendingAndInprogressServices();
            if (!services.Any())
            {
                return NotFound("No pending/in_progress orders ");
            }

            return Ok(services);
        }


        //OVO CES MORATI DA MENJAS DA IMAS DETALJE I O TASKOVIMA I RADNICIMA KOJI RADE NA NJIMA
        [HttpGet("{serviceId}")]
        public async Task<ActionResult<Service>> GetServiceById(Guid serviceId) 
        {
            var existingService = await _serviceService.GetServiceById(serviceId);
            if (existingService == null)
            {
                return NotFound();
            }

            return Ok(existingService);

        }

        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateService(Guid userId, [FromBody] CreateServiceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdService = await _serviceService.CreateService(userId, dto);

                if (createdService == null) return NotFound("Id not found");

                return Ok(createdService);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("accept/{serviceId}")]
        public async Task<IActionResult> AcceptService(Guid serviceId)
        {
            await _serviceService.AcceptService(serviceId);
            return Ok("Service prihvacen.");
        }

        [HttpPut("decline/{serviceId}")]
        public async Task<IActionResult> DeclineService(Guid serviceId, [FromBody] DeleteServiceDto dto)
        {
            await _serviceService.DeclineService(serviceId, dto);
            return Ok("Service ODBIJEN.");
        }

        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> DeleteService(Guid serviceId)
        {
            try
            {
                await _serviceService.DeleteService(serviceId);
                return Ok(" deleted successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
