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
        public async Task<ActionResult<PriceListInfo>> GetPriceListById(Guid id)
        {
            var existingEquipment = await _priceListService.GetPriceListById(id);
            if (existingEquipment == null)
            {
                return NotFound();
            }

            return Ok(existingEquipment);
        }
      

        [HttpPost("create/{productId}")] //NAMESTI DA TI VRACA RESPONSE KOJI IMA PODATKE O NASTALOM PRICELISTI + MOTOR.NAME + PRODUCER.NAME
        public async Task<ActionResult> CreatePriceList(Guid productId, [FromBody] CreatePriceListDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _priceListService.CreatePriceList(productId, dto);
                if (!created)
                    return NotFound("Id not found");

                return Ok("Price list created successfully");
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
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
