using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.MotorcycleDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleController : ControllerBase
    {
        private readonly MotorcycleService motorcycleService;

        public MotorcycleController(MotorcycleService motorcycleService)
        {
            this.motorcycleService = motorcycleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotorcycleInfo>>> GetAllMotorsPagination(int pageNumber = 1, int pageSize = 5)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                return BadRequest("Page size must be greater than 0.");
            }

            var pagedMotor = await motorcycleService.GetAllMotorsPagination(pageNumber, pageSize);
            return Ok(pagedMotor);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<MotorcycleInfo>> GetMotorById(Guid id)
        {
            var existingMotor = await motorcycleService.GetMotorById(id);
            if (existingMotor == null)
            {
                return NotFound();
            }

            return Ok(existingMotor);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMotor([FromBody] CreateMotorcycleDto createMotorDto)
        {
            if (createMotorDto == null)
            {
                return BadRequest("Motor data is required");
            }

            var createdMotor = await motorcycleService.CreateMotor(createMotorDto);
            return CreatedAtAction(nameof(GetMotorById), new { id = createdMotor.Id }, createdMotor);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateMotor(Guid id, [FromBody] UpdateMotorcycleDto updateMotorDto)
        {
            if (updateMotorDto == null)
            {
                return BadRequest("Motor data is required");
            }

            try
            {
                await motorcycleService.UpdateMotor(id, updateMotorDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMotor(Guid id)
        {
            var existingMotor = await motorcycleService.GetMotorById(id);
            if (existingMotor == null)
            {
                return NotFound("Motor doesn't exist");
            }

            await motorcycleService.DeleteMotor(id);
            return NoContent();
        }
    }
}
