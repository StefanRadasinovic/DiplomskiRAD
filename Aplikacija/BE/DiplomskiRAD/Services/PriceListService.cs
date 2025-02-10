
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
        private readonly EquipmentRepository _equipmentRepository;

        public PriceListService(PriceListRepository priceListRepository, MotorcycleRepository motorcycleRepository, EquipmentRepository equipmentRepository)
        {
            _priceListRepository = priceListRepository;
            _motorcycleRepository = motorcycleRepository;
            _equipmentRepository = equipmentRepository;
        }



        public async Task<CustomPriceListInfo> GetPriceListById(Guid id)
        {
            var priceList = await _priceListRepository.GetPriceListById(id);
            if (priceList == null)
            {
                throw new Exception("Price list doesn't exist");
            }

            var motorcycles = priceList.Motorcycles ?? new List<Motorcycle>();
            var equipments = priceList.Equipments ?? new List<Equipment>();

            var productList = new List<object>();

         
            if (motorcycles.Any())
            {
                productList.AddRange(motorcycles.Select(m => new MotorcycleDTO.JustMotorcycleName(
                    m.Id,
                    m.Name,
                    m.Producers?.Select(p => new ProducerInfo
                    {
                        Name = p.Name,
                        Description = p.Description
                    }).ToList() ?? new List<ProducerInfo>() 
                )));
            }

            if (equipments.Any())
            {
                productList.AddRange(equipments.Select(e => new EquipmentDTO.JustEquipmentName(
                    e.Id,
                    e.Name,
                    e.Producers?.Select(p => new ProducerInfo
                    {
                        Name = p.Name,
                        Description = p.Description
                    }).ToList() ?? new List<ProducerInfo>() 
                )));
            }

            return new CustomPriceListInfo(
                priceList.Id,
                priceList.Price,
                priceList.StartingDate.ToString("yyyy-MM-dd"),
                priceList.EndingDate.ToString("yyyy-MM-dd"),
                productList 
            );
        }



        public async Task<CustomPriceListInfo?> CreatePriceList(Guid productId, CreatePriceListDto dto)
        {
            PriceList priceList;

            var motorcycle = await _motorcycleRepository.GetMotorById(productId);
            if (motorcycle == null)
            {
                var equipment = await _equipmentRepository.GetEquipmentById(productId);
                if (equipment == null)
                {
                    return null;
                }

                // Validate date formats
                if (!DateTime.TryParseExact(dto.StartingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate2))
                    throw new FormatException("Invalid Starting Date format. Use yyyy-MM-dd.");

                if (!DateTime.TryParseExact(dto.EndingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate2))
                    throw new FormatException("Invalid Ending Date format. Use yyyy-MM-dd.");

                // Convert to UTC
                startDate2 = DateTime.SpecifyKind(startDate2, DateTimeKind.Utc);
                endDate2 = DateTime.SpecifyKind(endDate2, DateTimeKind.Utc);

                priceList = new PriceList
                {
                    Id = Guid.NewGuid(),
                    Price = dto.Price,
                    StartingDate = startDate2,
                    EndingDate = endDate2,
                    Equipments = new List<Equipment> { equipment }
                };

                if(priceList.EndingDate <= priceList.StartingDate)
                {
                    throw new Exception("EndingDate ne moze biti <= StartingDate");
                }

                await _priceListRepository.CreatePriceList(priceList);

                return new CustomPriceListInfo(
                    priceList.Id,
                    priceList.Price,
                    priceList.StartingDate.ToString("yyyy-MM-dd"),
                    priceList.EndingDate.ToString("yyyy-MM-dd"),
                    priceList.Equipments
                    .Select(e => new JustEquipmentName(
                    e.Id,
                    e.Name,
                    e.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
                    ))
                    .Cast<object>() 
                    .ToList()
                );
            }

            // Validate date formats
            if (!DateTime.TryParseExact(dto.StartingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                throw new FormatException("Invalid Starting Date format. Use yyyy-MM-dd.");

            if (!DateTime.TryParseExact(dto.EndingDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                throw new FormatException("Invalid Ending Date format. Use yyyy-MM-dd.");

            // Convert to UTC
            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            priceList = new PriceList
            {
                Id = Guid.NewGuid(),
                Price = dto.Price,
                StartingDate = startDate,
                EndingDate = endDate,
                Motorcycles = new List<Motorcycle> { motorcycle }
            };

            if (priceList.EndingDate <= priceList.StartingDate)
            {
                throw new Exception("EndingDate ne moze biti <= StartingDate");
            }

            await _priceListRepository.CreatePriceList(priceList);

            return new CustomPriceListInfo(
                priceList.Id,
                priceList.Price,
                priceList.StartingDate.ToString("yyyy-MM-dd"),
                priceList.EndingDate.ToString("yyyy-MM-dd"),
                priceList.Motorcycles.Select(m => new JustMotorcycleName(
                    m.Id,
                    m.Name,
                    m.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
                    )).Cast<object>() 
                    .ToList()
            );
        }



        public async Task<PriceListInfo?> GetCurrentPriceListByMotorId(Guid motorcycleId)
        {
            var priceList = await _priceListRepository.GetCurrentPriceListByMotorId(motorcycleId);
            if (priceList == null)
            {
                return null;
            }

            var motorcycleList = priceList.Motorcycles.Select(m => new MotorcycleDTO.JustMotorcycleName(
               m.Id,
               m.Name,
               m.Producers.Select(p => new ProducerInfo
               {
                   Name = p.Name,
                   Description = p.Description
               }).ToList())).ToList();


            return new PriceListInfo(
                 priceList.Id,
                 priceList.Price,
                 priceList.StartingDate.ToString("yyyy-MM-dd"),
                 priceList.EndingDate.ToString("yyyy-MM-dd"),
                 motorcycleList
            );
        }


        public async Task<List<PriceListInfo>> GetAllPricesListByMotorId(Guid motorcycleId)
        {
            var priceLists = await _priceListRepository.GetAllPricesListByMotorId(motorcycleId);

            return priceLists.Select(p => new PriceListInfo(
                p.Id,
                p.Price,
                p.StartingDate.ToString("yyyy-MM-dd"),
                p.EndingDate.ToString("yyyy-MM-dd"),
                p.Motorcycles.Select(m => new JustMotorcycleName(
                    m.Id,
                    m.Name,
                    m.Producers.Select(prod => new ProducerInfo
                    {
                        Name = prod.Name,
                        Description = prod.Description
                    }).ToList()
                )).ToList()
            )).ToList();
        }

        public async Task<PriceListInfo22?> GetCurrentPriceListByEquipmentId(Guid equipmentId)
        {
            var priceList = await _priceListRepository.GetCurrentPriceListByEquipmentId(equipmentId);
            if (priceList == null)
            {
                return null;
            }

            var equipmentList = priceList.Equipments.Select(m => new EquipmentDTO.JustEquipmentName(
               m.Id,
               m.Name,
               m.Producers.Select(p => new ProducerInfo
               {
                   Name = p.Name,
                   Description = p.Description
               }).ToList())).ToList();


            return new PriceListInfo22(
                 priceList.Id,
                 priceList.Price,
                 priceList.StartingDate.ToString("yyyy-MM-dd"),
                 priceList.EndingDate.ToString("yyyy-MM-dd"),
                 equipmentList
            );
        }


        public async Task<List<PriceListInfo22>> GetAllPricesListByEquipmentId(Guid equipmentId)
        {
            var priceLists = await _priceListRepository.GetAllPricesListByEquipmentId(equipmentId);

            return priceLists.Select(p => new PriceListInfo22(
                p.Id,
                p.Price,
                p.StartingDate.ToString("yyyy-MM-dd"),
                p.EndingDate.ToString("yyyy-MM-dd"),
                p.Equipments.Select(m => new JustEquipmentName(
                    m.Id,
                    m.Name,
                    m.Producers.Select(prod => new ProducerInfo
                    {
                        Name = prod.Name,
                        Description = prod.Description
                    }).ToList()
                )).ToList()
            )).ToList();
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

    }
}
