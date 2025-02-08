using System.Linq;
using System.Text.Json.Serialization;
using DiplomskiRAD.DTOs;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using Microsoft.EntityFrameworkCore;
using static DiplomskiRAD.DTOs.MotorcycleDTO;
using static DiplomskiRAD.DTOs.ProducerDTO;

namespace DiplomskiRAD.Services
{
    public class MotorcycleService
    {
        private readonly MotorcycleRepository _motorcycleRepository;
        private readonly ProducerRepository _producerRepository;

        public MotorcycleService(MotorcycleRepository motorcycleRepository, ProducerRepository producerRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _producerRepository = producerRepository;
        }

        public async Task<PageResponseOffset<MotorcycleInfo>> GetAllMotorsPagination(int pageNumber, int pageSize)
        {
            var count = (await _motorcycleRepository.GetAllAsync()).Count();
            var data = await _motorcycleRepository.GetWithOffsetPagination(pageNumber, pageSize);

            var motorInfos = data.Select(motor => new MotorcycleDTO.MotorcycleInfo(
                motor.Id,
                motor.Name,
                motor.Slika,
                motor.Kilometraza,
                motor.YearOfProduction,
                motor.MotorcycleState,
                motor.Amount,
                motor.MotorcycleType,
                motor.Producers.Select(p => new ProducerDTO.ProducerInfo
                {
                    Name = p.Name,
                    Description = p.Description
                }).ToList(),
                motor.PriceLists.Select(p => new PriceListDTO.DisplayPriceOnly(p.Price)).ToList() 
            )).ToList();

            var response = new PageResponseOffset<MotorcycleInfo>(motorInfos, pageNumber, pageSize, count);
            return response;
        }



        public async Task<MotorcycleInfo> GetMotorById(Guid id)
        {
            var motor = await _motorcycleRepository.GetMotorById(id);

            if (motor == null)
            {
                throw new Exception("Motor doesn't exist");
            }

            return new MotorcycleInfo(
                motor.Id,
                motor.Name,
                motor.Slika,
                motor.Kilometraza,  
                motor.YearOfProduction,
                motor.MotorcycleState,
                motor.Amount, 
                motor.MotorcycleType,
                motor.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList(),
                motor.PriceLists.Select(p => new PriceListDTO.DisplayPriceOnly(p.Price)).ToList()
            );
        }


        public async Task<Motorcycle> CreateMotor(CreateMotorcycleDto motorDto)
        {

            var producers = new List<Producer>();

            foreach (var p in motorDto.Producers)
            {
                var existingProducer = await _producerRepository.GetProducerByName(p.Name);

                if (existingProducer != null)
                {
                    producers.Add(existingProducer);
                }
                else
                {
                    var newProducer = new Producer
                    {
                        Id = Guid.NewGuid(), 
                        Name = p.Name,
                        Description = p.Description
                    };

                    producers.Add(newProducer);
                }
            }

            var motor = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Name = motorDto.Name,
                Slika = motorDto.Slika,
                Kilometraza = motorDto.Kilometraza,
                YearOfProduction = motorDto.YearOfProduction,
                MotorcycleState = motorDto.MotorcycleState,
                Amount = motorDto.Amount,
                MotorcycleType = motorDto.MotorcycleType,
                Producers = producers 
            };

            _motorcycleRepository.CreateMotor(motor);
            return motor;

        }


        public async Task UpdateMotor(Guid id, UpdateMotorcycleDto motorDto)
        {
            var existingMotor = await _motorcycleRepository.GetMotorById(id);

            if (existingMotor == null)
            {
                throw new Exception("Motor doesn't exist");
            }

            existingMotor.Name = motorDto.Name ?? existingMotor.Name;
            existingMotor.Slika = motorDto.Slika ?? existingMotor.Slika;
            existingMotor.Kilometraza = motorDto.Kilometraza;
            existingMotor.YearOfProduction = motorDto.YearOfProduction;
            existingMotor.MotorcycleState = motorDto.MotorcycleState;
            existingMotor.Amount = motorDto.Amount;
            existingMotor.MotorcycleType = motorDto.MotorcycleType;

            if (existingMotor.Producers == null)
            {
                existingMotor.Producers = new List<Producer>();
            }

            var updatedProducers = new List<Producer>();

            foreach (var producerDto in motorDto.Producers)
            {
                var existingProducer = await _producerRepository.GetProducerByName(producerDto.Name);

                if (existingProducer != null)
                {
                    existingProducer.Description = producerDto.Description;
                    updatedProducers.Add(existingProducer);
                }
                else
                {
                    var newProducer = new Producer
                    {
                        Id = Guid.NewGuid(),
                        Name = producerDto.Name,
                        Description = producerDto.Description
                    };

                    await _producerRepository.CreateProducer(newProducer);
                    updatedProducers.Add(newProducer);
                }
            }

            existingMotor.Producers = updatedProducers;
            await _motorcycleRepository.UpdateMotor(existingMotor);
        }


        public async Task DeleteMotor(Guid id)
        {
            await _motorcycleRepository.DeleteMotor(id);
        }

        public async Task<IEnumerable<MotorcycleInfo>> GetMotorsByName(string name)
        {
            var motorcycles = await _motorcycleRepository.GetMotorsByName(name);
            return motorcycles.Select(m => new MotorcycleDTO.MotorcycleInfo(
                m.Id,
                m.Name,
                m.Slika,
                m.Kilometraza,
                m.YearOfProduction,
                m.MotorcycleState,
                m.Amount,
                m.MotorcycleType,
                m.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
            ));
        }

        public async Task<IEnumerable<MotorcycleInfo>> GetMotorsByProducerName(string producerName)
        {
            var motorcycles = await _motorcycleRepository.GetMotorsByProducerName(producerName);
            return motorcycles.Select(m => new MotorcycleDTO.MotorcycleInfo(
                m.Id,
                m.Name,
                m.Slika,
                m.Kilometraza,
                m.YearOfProduction,
                m.MotorcycleState,
                m.Amount,
                m.MotorcycleType,
                m.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
            ));
        }
    }
}
