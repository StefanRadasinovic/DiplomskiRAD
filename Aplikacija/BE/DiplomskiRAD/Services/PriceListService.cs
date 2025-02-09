
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



        public async Task<PriceListInfo> GetPriceListById(Guid id)
        {
            var priceList = await _priceListRepository.GetPriceListById(id);
            if (priceList == null)
            {
                throw new Exception("Price list doesn't exist");
            }

            var motorcycleList = priceList.Motorcycles.Select(m => new MotorcycleDTO.JustMotorcycleName(
              m.Id,
              m.Name,
              m.Producers.Select(p => new ProducerInfo { 
                  Name = p.Name, 
                  Description = p.Description 
              }).ToList())).ToList();


            return new PriceListInfo(
                 priceList.Id,
                 priceList.Price,
                 priceList.StartingDate.ToString("dd/MM/yyyy"),
                 priceList.EndingDate.ToString("dd/MM/yyyy"),
                 motorcycleList
            );
        }
        
        public async Task<bool> CreatePriceList(Guid productId, CreatePriceListDto dto)
        {
            var motorcycle = await _motorcycleRepository.GetMotorById(productId);
            if (motorcycle == null)
            {
                var equipment = await _equipmentRepository.GetEquipmentById(productId);
                if (equipment == null)
                {
                    return false;
                }

                if (!DateTime.TryParseExact(dto.StartingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate2))
                {
                    throw new FormatException("Invalid Starting Date format. Use dd/MM/yyyy.");
                }

                if (!DateTime.TryParseExact(dto.EndingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate2))
                {
                    throw new FormatException("Invalid Ending Date format. Use dd/MM/yyyy.");
                }

                // converted to UTC
                startDate2 = DateTime.SpecifyKind(startDate2, DateTimeKind.Utc);
                endDate2 = DateTime.SpecifyKind(endDate2, DateTimeKind.Utc);

                var priceList2 = new PriceList
                {
                    Id = Guid.NewGuid(),
                    Price = dto.Price,
                    StartingDate = startDate2,
                    EndingDate = endDate2,
                    Equipments = new List<Equipment> { equipment }
                };

                await _priceListRepository.CreatePriceList(priceList2);
                return true;
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
                 priceList.StartingDate.ToString("dd/MM/yyyy"),
                 priceList.EndingDate.ToString("dd/MM/yyyy"),
                 motorcycleList
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
                 priceList.StartingDate.ToString("dd/MM/yyyy"),
                 priceList.EndingDate.ToString("dd/MM/yyyy"),
                 equipmentList
            );
        }


        public async Task<List<PriceListInfo22>> GetAllPricesListByEquipmentId(Guid equipmentId)
        {
            var priceLists = await _priceListRepository.GetAllPricesListByEquipmentId(equipmentId);

            return priceLists.Select(p => new PriceListInfo22(
                p.Id,
                p.Price,
                p.StartingDate.ToString("dd/MM/yyyy"),
                p.EndingDate.ToString("dd/MM/yyyy"),
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
