<<<<<<< Updated upstream
﻿using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.PriceListDTO;
using System.Globalization;
using DiplomskiRAD.Models;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.ProducerDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;
using System.Linq;
using DiplomskiRAD.DTOs;

namespace DiplomskiRAD.Services
{
    public class PriceListService
    {
        private readonly PriceListRepository _priceListRepository;
        private readonly MotorcycleRepository _motorcycleRepository;

        public PriceListService(PriceListRepository priceListRepository, MotorcycleRepository motorcycleRepository)
        {
            _priceListRepository = priceListRepository;
            _motorcycleRepository = motorcycleRepository;
        }



        public async Task<PriceListInfo> GetPriceListById(Guid id)
        {
            var priceList = await _priceListRepository.GetPriceListById(id);
            if (priceList == null)
            {
                throw new Exception("Price list doesn't exist");
            }

            return new PriceListInfo(
                 priceList.Id,
                 priceList.Price,
                 priceList.StartingDate.ToString("dd/MM/yyyy"), // Convert DateTime to string
                 priceList.EndingDate.ToString("dd/MM/yyyy")
            );
        }

        public async Task<PriceListInfo?> GetCurrentPriceListByMotorId(Guid motorcycleId)
        {
            var currentPriceList = await _priceListRepository.GetCurrentPriceListByMotorId(motorcycleId);
            if (currentPriceList == null)
            {
                return null;
            }

            return new PriceListInfo(
                currentPriceList.Id,
                currentPriceList.Price,
                currentPriceList.StartingDate.ToString("dd/MM/yyyy"),
                currentPriceList.EndingDate.ToString("dd/MM/yyyy"),
                currentPriceList.Motorcycles.Select(m => new MotorcycleInfo
                {
                    Name = m.Name,
                    Producers = m.Producers.Select(p => new ProducerInfo{Name = p.Name,}).ToList()}).ToList()
            );
        }


        public async Task<List<PriceListInfo>> GetAllPricesListByMotorId(Guid motorcycleId)
        {
            var priceLists = await _priceListRepository.GetAllPricesListByMotorId(motorcycleId);

            return priceLists.Select(p => new PriceListInfo(
                p.Id,
                p.Price,
                p.StartingDate.ToString("dd/MM/yyyy"),
                p.EndingDate.ToString("dd/MM/yyyy"),
                p.Motorcycles.Select(m => new MotorcycleInfo
                {
                    Name = m.Name,
                    Producers = m.Producers.Select(prod => new ProducerInfo{Name = prod.Name,}).ToList()}).ToList()
                )).ToList();
        }



        public async Task<bool> CreatePriceList(Guid motorcycleId, CreatePriceListDto dto)
        {
            var motorcycle = await _motorcycleRepository.GetMotorById(motorcycleId);
            if (motorcycle == null)
            {
                return false;
            }

            if (!DateTime.TryParseExact(dto.StartingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                throw new FormatException("Invalid Starting Date format. Use dd/MM/yyyy.");
            }

            if (!DateTime.TryParseExact(dto.EndingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                throw new FormatException("Invalid Ending Date format. Use dd/MM/yyyy.");
            }

            // converted to UTC
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            var priceList = new PriceList
            {
                Id = Guid.NewGuid(),
                Price = dto.Price,
                StartingDate = startDate,
                EndingDate = endDate,
                Motorcycles = new List<Motorcycle> { motorcycle }
            };

            await _priceListRepository.CreatePriceList(priceList);
            return true;
        }



        /*
        public async Task<bool> UpdatePriceList(Guid motorcycleId, UpdatePriceListDto dto)
        {
            var priceList = await _priceListRepository.GetPriceListById(dto.Id);
            if (priceList == null)
            {
                return false;
            }

            var motorcycle = await _motorcycleRepository.GetMotorById(motorcycleId);
            if (motorcycle == null || !priceList.Motorcycles.Contains(motorcycle))
            {
                return false;
            }

            priceList.Price = dto.Price;
            priceList.StartingDate = DateTime.ParseExact(dto.StartingDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            priceList.EndingDate = DateTime.ParseExact(dto.EndingDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            await _priceListRepository.UpdatePriceList(priceList);
            return true;
        }
        */
=======
﻿namespace DiplomskiRAD.Services
{
    public class PriceListService
    {
>>>>>>> Stashed changes
    }
}
