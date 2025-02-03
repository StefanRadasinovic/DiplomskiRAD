using DiplomskiRAD.DTOs;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;

namespace DiplomskiRAD.Services
{
    public class EquipmentService
    {
        private readonly EquipmentRepository _equipmentRepository;

        public EquipmentService(EquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository; 
        }


        public async Task<PageResponseOffset<EquipmentInfo>> GetAllEquipmentPagination(int pageNumber, int pageSize)
        {
            var count = (await _equipmentRepository.GetAllAsync()).Count();
            var data = await _equipmentRepository.GetWithOffsetPagination(pageNumber, pageSize);

            var equipmentInfos = data.Select(eq => new EquipmentInfo
            (eq.Id, eq.Name, eq.Slika)).ToList();
            var response = new PageResponseOffset<EquipmentInfo>((List<EquipmentInfo>)equipmentInfos, pageNumber, pageSize, count);
            return response;
        }


        public async Task<Equipment> GetEquipmentById(Guid id)
        {
            var motor = await _equipmentRepository.GetEquipmentById(id);
            if (motor == null)
            {
                throw new Exception("Equipment doesn't exist");
            }

            return motor;
        }


        public async Task<Equipment> CreateEquipment(CreateEquipmentDTO equipmentDTO)
        {

            var eq = new Equipment
            {
                Id = Guid.NewGuid(),
                Name = equipmentDTO.Name,
                Slika = equipmentDTO.Slika,
                Amount = equipmentDTO.Amount,

            };

            await _equipmentRepository.CreateEquipment(eq);
            return eq;
        }

        public async Task UpdateEquipment(Guid id, UpdateEquipmentDTO updateEquipmentDTO)
        {
            var existingEquipment = await _equipmentRepository.GetEquipmentById(id);

            if (existingEquipment == null)
            {
                throw new Exception("Equipment doesn't exist");
            }

            existingEquipment.Name = updateEquipmentDTO.Name ?? existingEquipment.Name;
            existingEquipment.Slika = updateEquipmentDTO.Slika ?? existingEquipment.Slika;
            existingEquipment.Amount = updateEquipmentDTO.Amount;

            await _equipmentRepository.UpdateEquipment(existingEquipment);
        }

        public async Task DeleteEquipment(Guid id)
        {
            await _equipmentRepository.DeleteEquipment(id);
        }

    }
}
