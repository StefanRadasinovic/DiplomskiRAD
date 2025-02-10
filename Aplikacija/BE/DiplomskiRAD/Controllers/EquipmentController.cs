using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : Controller   
    {
        private readonly EquipmentService equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentInfo>>> GetAllEquipmentPagination(int pageNumber = 1, int pageSize = 5)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                return BadRequest("Page size must be greater than 0.");
            }

            var pagedEquipment = await equipmentService.GetAllEquipmentPagination(pageNumber, pageSize);
            return Ok(pagedEquipment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentInfo>> GetEquipmentById(Guid id)
        {
            var existingEquipment = await equipmentService.GetEquipmentById(id);
            if (existingEquipment == null)
            {
                return NotFound();
            }

            return Ok(existingEquipment);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEquipment([FromBody] CreateEquipmentDTO createEquipmentDTO)
        {
            if (createEquipmentDTO == null)
            {
                return BadRequest("Equipment data is required");
            }

            var createdEquipment = await equipmentService.CreateEquipment(createEquipmentDTO);
            return CreatedAtAction(nameof(GetEquipmentById), new { id = createdEquipment.Id }, createdEquipment);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateEquipment(Guid id, [FromBody] UpdateEquipmentDTO updateEquipmentDTO)
        {
            if (updateEquipmentDTO == null)
            {
                return BadRequest("Equipment data is required");
            }

            try
            {
                await equipmentService.UpdateEquipment(id, updateEquipmentDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEquipment(Guid id)
        {
            var existingEquipment = await equipmentService.GetEquipmentById(id);
            if (existingEquipment == null)
            {
                return NotFound("Equipment doesn't exist");
            }

            await equipmentService.DeleteEquipment(id);
            return NoContent();
        }


        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<EquipmentInfo>>> GetEquipmentByName(string name)
        {
            var result = await equipmentService.GetEquipmentByName(name);
            return Ok(result);
        }

        [HttpGet("producer/{producerName}")]
        public async Task<ActionResult<IEnumerable<EquipmentInfo>>> GetEquipmentByProducerName(string producerName)
        {
            var result = await equipmentService.GetEquipmentByProducerName(producerName);
            return Ok(result);
        }

        //kombinacija name+producerName
        [HttpGet("name-producer")]
        public async Task<ActionResult<IEnumerable<EquipmentInfo>>> GetEquipmentByNameAndProducerName(string name, string producerName)
        {
            var result = await equipmentService.GetEquipmentByNameAndProducerName(name, producerName);
            return Ok(result);
        }
    }
}
