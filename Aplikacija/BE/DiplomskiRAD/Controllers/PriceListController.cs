using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
using static DiplomskiRAD.DTOs.EquipmentDTO;
=======
>>>>>>> Stashed changes
using static DiplomskiRAD.DTOs.PriceListDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
<<<<<<< Updated upstream
    public class PriceListController : Controller
    {

        private readonly PriceListService _priceListService;

        public PriceListController(PriceListService priceListService)
=======
    public class PriceListController : ControllerBase
    {
        private readonly PriceListService _priceListService;
        
        public PriceListController(PriceListService priceListService) 
>>>>>>> Stashed changes
        {
            _priceListService = priceListService;
        }

<<<<<<< Updated upstream

        [HttpGet("{id}")]
        public async Task<ActionResult<PriceListInfo>> GetPriceListById(Guid id)
        {
            var existingEquipment = await _priceListService.GetPriceListById(id);
            if (existingEquipment == null)
            {
                return NotFound();
            }

            return Ok(existingEquipment);
        }

        [HttpGet("currentPrice/{motorcycleId}")]
        public async Task<IActionResult> GetCurrentPriceListByMotorId(Guid motorcycleId)
        {
            var price = await _priceListService.GetCurrentPriceListByMotorId(motorcycleId);
            if (price == null)
            {
                return NotFound("No current price found for this motorcycle");
            }
                
            return Ok(price);
        }

        [HttpGet("allPrices/{motorcycleId}")]
        public async Task<IActionResult> GetAllPricesListByMotorId(Guid motorcycleId)
        {
            var priceInfos = await _priceListService.GetAllPricesListByMotorId(motorcycleId);
            if (!priceInfos.Any())
                return NotFound("No price history found for this motorcycle");

            return Ok(priceInfos);
        }


=======
>>>>>>> Stashed changes
        [HttpPost("create/{motorcycleId}")]
        public async Task<ActionResult> CreatePriceList(Guid motorcycleId, [FromBody] CreatePriceListDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _priceListService.CreatePriceList(motorcycleId, dto);
                if (!created)
                    return NotFound("Motorcycle not found");

                return Ok("Price list created successfully");
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

<<<<<<< Updated upstream

        /*
        [HttpPut("update/{motorcycleId}")]
        public async Task<IActionResult> UpdatePriceList(Guid motorcycleId, [FromBody] UpdatePriceListDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _priceListService.UpdatePriceList(motorcycleId, dto);
            if (!updated)
                return NotFound("Motorcycle or Price list not found");

            return Ok("Price list updated successfully");
        }
        */

=======
>>>>>>> Stashed changes
    }
}
