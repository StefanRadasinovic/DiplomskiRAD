using System.Xml.Linq;
using DiplomskiRAD.DTOs;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.EquipmentDTO;
using static DiplomskiRAD.DTOs.MotorcycleDTO;
using static DiplomskiRAD.DTOs.ProducerDTO;

namespace DiplomskiRAD.Services
{
    public class EquipmentService
    {
        private readonly EquipmentRepository _equipmentRepository;
        private readonly ProducerRepository _producerRepository;

        public EquipmentService(EquipmentRepository equipmentRepository, ProducerRepository producerRepository)
        {
            _equipmentRepository = equipmentRepository;
            _producerRepository = producerRepository;
        }


        public async Task<PageResponseOffset<EquipmentInfo>> GetAllEquipmentPagination(int pageNumber, int pageSize)
        {
            var count = (await _equipmentRepository.GetAllAsync()).Count();
            var data = await _equipmentRepository.GetWithOffsetPagination(pageNumber, pageSize);

            var equipmentInfos = data.Select(eq => new EquipmentInfo
            (eq.Id, eq.Name, eq.Slika, eq.EquipmentState, eq.Amount, eq.Producers.Select(p => new ProducerDTO.ProducerInfo 
            { Name = p.Name, Description = p.Description }).ToList())).ToList();
            var response = new PageResponseOffset<EquipmentInfo>((List<EquipmentInfo>)equipmentInfos, pageNumber, pageSize, count);
            return response;

        }


        public async Task<EquipmentInfo> GetEquipmentById(Guid id)
        {
            var eq = await _equipmentRepository.GetEquipmentById(id);
            if (eq == null)
            {
                throw new Exception("Equipment doesn't exist");
            }

            return new EquipmentInfo(
                eq.Id,
                eq.Name,
                eq.Slika,
                eq.EquipmentState,
                eq.Amount,
                eq.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
            );
        }

        public async Task<IEnumerable<EquipmentInfo>> GetEquipmentByName(string name)
        {
            var equipment = await _equipmentRepository.GetEquipmentByName(name);
            return equipment.Select(m => new EquipmentDTO.EquipmentInfo(
                m.Id,
                m.Name,
                m.Slika,
                m.EquipmentState,
                m.Amount,
                m.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
            ));
        }

        public async Task<IEnumerable<EquipmentInfo>> GetEquipmentByProducerName(string producerName)
        {
            var equipment = await _equipmentRepository.GetEquipmentByProducerName(producerName);
            return equipment.Select(m => new EquipmentDTO.EquipmentInfo(
                m.Id,
                m.Name,
                m.Slika,
                m.EquipmentState,
                m.Amount,
                m.Producers.Select(p => new ProducerInfo(p.Name, p.Description)).ToList()
            ));
        }


        public async Task<Equipment> CreateEquipment(CreateEquipmentDTO equipmentDTO)
        {
            var producers = new List<Producer>();

            foreach (var p in equipmentDTO.Producers)
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

            var equipment = new Equipment
            {
                Id = Guid.NewGuid(),
                Name = equipmentDTO.Name,
                Slika = equipmentDTO.Slika,
                Amount =equipmentDTO.Amount,
                EquipmentState= equipmentDTO.EquipmentState,
                Producers = producers
            };

            _equipmentRepository.CreateEquipment(equipment);
            return equipment;



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
            existingEquipment.EquipmentState = updateEquipmentDTO.EquipmentState;
            existingEquipment.Amount = updateEquipmentDTO.Amount;

            if (existingEquipment.Producers == null)
            {
                existingEquipment.Producers = new List<Producer>();
            }

            var updatedProducers = new List<Producer>();

            foreach (var producerDto in updateEquipmentDTO.Producers)
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

            existingEquipment.Producers = updatedProducers;
            await _equipmentRepository.UpdateEquipment(existingEquipment);
        }

        public async Task DeleteEquipment(Guid id)
        {
            await _equipmentRepository.DeleteEquipment(id);
        }

    }
}
