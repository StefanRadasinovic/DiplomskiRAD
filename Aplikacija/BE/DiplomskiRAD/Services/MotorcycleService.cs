using System.Text.Json.Serialization;
using DiplomskiRAD.DTOs;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.MotorcycleDTO;

namespace DiplomskiRAD.Services
{
    public class MotorcycleService
    {
        private readonly MotorcycleRepository _motorcycleRepository;

        public MotorcycleService(MotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<PageResponseOffset<MotorcycleInfo>> GetAllMotorsPagination(int pageNumber, int pageSize)
        {
            var count = (await _motorcycleRepository.GetAllAsync()).Count();
            var data = await _motorcycleRepository.GetWithOffsetPagination(pageNumber, pageSize);

            var motorInfos = data.Select(motor => new MotorcycleInfo
            (motor.Id, motor.Name, motor.MotorcycleType, motor.YearOfProduction, motor.Slika)).ToList();
            var response = new PageResponseOffset<MotorcycleInfo>((List<MotorcycleInfo>)motorInfos, pageNumber, pageSize, count);
            return response;
        }


        public async Task<Motorcycle> GetMotorById(Guid id)
        {
            var motor = await _motorcycleRepository.GetMotorById(id);
            if (motor == null)
            {
                throw new Exception("Motor doesn't exist");
            }

            return motor;
        }

        public async Task<Motorcycle> CreateMotor(CreateMotorcycleDto motorDto)
        {

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

            };


            await _motorcycleRepository.CreateMotor(motor);
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

            await _motorcycleRepository.UpdateMotor(existingMotor);
        }

        public async Task DeleteMotor(Guid id)
        {
            await _motorcycleRepository.DeleteMotor(id);
        }
    }
}
