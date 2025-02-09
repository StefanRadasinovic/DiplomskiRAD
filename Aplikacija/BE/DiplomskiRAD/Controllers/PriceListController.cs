using DiplomskiRAD.Services;
using Microsoft.AspNetCore.Mvc;
using static DiplomskiRAD.DTOs.EquipmentDTO;

using static DiplomskiRAD.DTOs.PriceListDTO;

namespace DiplomskiRAD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : Controller
    {

        private readonly PriceListService _priceListService;
        
        public PriceListController(PriceListService priceListService) 
        {
            _priceListService = priceListService;
        }


        [HttpGet("{id}")] 
        public async Task<ActionResult<CustomPriceListInfo>> GetPriceListById(Guid id)
        {
            var existingEquipment = await _priceListService.GetPriceListById(id);
            if (existingEquipment == null)
            {
                return NotFound();
            }

            return Ok(existingEquipment);
        }


        [HttpPost("create/{productId}")]
        public async Task<ActionResult> CreatePriceList(Guid productId, [FromBody] CreatePriceListDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdPriceList = await _priceListService.CreatePriceList(productId, dto);
                if (createdPriceList == null)
                    return NotFound("Id not found");

                return Ok(createdPriceList);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("MotorCurrentPrice/{motorcycleId}")]
       public async Task<IActionResult> GetCurrentPriceListByMotorId(Guid motorcycleId)
       {
           var price = await _priceListService.GetCurrentPriceListByMotorId(motorcycleId);
           if (price == null)
           {
               return NotFound("No current price found for this motorcycle");
           }

           return Ok(price);
       }

       [HttpGet("MotorAllPrices/{motorcycleId}")]
       public async Task<IActionResult> GetAllPricesListByMotorId(Guid motorcycleId)
       {
           var priceInfos = await _priceListService.GetAllPricesListByMotorId(motorcycleId);
           if (!priceInfos.Any())
               return NotFound("No price history found for this motorcycle");

           return Ok(priceInfos);
       }

        [HttpGet("EquipmentCurrentPrice/{equipmentId}")]
        public async Task<IActionResult> GetCurrentPriceListByEquipmentId(Guid equipmentId)
        {
            var price = await _priceListService.GetCurrentPriceListByEquipmentId(equipmentId);
            if (price == null)
            {
                return NotFound("No current price found for this equipment");
            }

            return Ok(price);
        }

        [HttpGet("EquipmentAllPrices/{equipmentId}")]
        public async Task<IActionResult> GetAllPricesListByEquipmentId(Guid equipmentId)
        {
            var priceInfos = await _priceListService.GetAllPricesListByEquipmentId(equipmentId);
            if (!priceInfos.Any())
                return NotFound("No price history found for this equipment");

            return Ok(priceInfos);
        }



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

    }
}
