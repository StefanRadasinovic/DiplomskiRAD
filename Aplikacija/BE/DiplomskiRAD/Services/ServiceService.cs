using System.Globalization;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DiplomskiRAD.DTOs.ServiceDTO;
using static DiplomskiRAD.DTOs.SparePartDTO;
using static DiplomskiRAD.DTOs.TaskServiceDTO;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.Services
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;
        private readonly UserRepository _userRepository;
        private readonly TaskRepository _tasRepository;

        public ServiceService(ServiceRepository serviceRepository, UserRepository userRepository, TaskRepository tasRepository)
        {
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _tasRepository = tasRepository;
        }

        public async Task<IEnumerable<UserServiceInfo>> GetAllServicesForUser(Guid userId)
        {
            var services = await _serviceRepository.GetAllServicesByUserId(userId);
            return services.Select(s => new UserServiceInfo
            {
                Id = s.Id,
                FailureDescription = s.FailureDescription,
                Picture = s.Picture,
                StartDate = s.StartDate.ToString("yyyy-MM-dd"),
                EndDate = s.EndDate?.ToString("yyyy-MM-dd"),
                ServiceStatus = s.ServiceStatus,
                razlogOdbijanja = s.razlogOdbijanja
            });
        }

        public async Task<IEnumerable<DirektorServiceInfo>> GetAllPendingAndInprogressServices()
        {
            var services = await _serviceRepository.GetAllPendingAndInprogressServices();

            return services.Select(s => new DirektorServiceInfo
            {
                Id = s.Id,
                FailureDescription = s.FailureDescription,
                Picture = s.Picture,
                StartDate = s.StartDate.ToString("yyyy-MM-dd"),
                EndDate = s.EndDate?.ToString("yyyy-MM-dd"),
                ServiceStatus = s.ServiceStatus,
                razlogOdbijanja = s.razlogOdbijanja,
                TaskServiceInfo = s.TaskServices?.Select(task => new TaskServiceInfo
                {
                    Id = task.Id,
                    TaskDescription = task.TaskDescription,
                    Status = task.Status,
                    EndDateTask = task.EndDateTask?.ToString("yyyy-MM-dd"),
                    RazlogOdbijanja = task.razlogOdbijanja,
                    WorkerInfo = task.User != null ? new UserInfo
                    {
                        Id = task.User.Id,
                        Name = task.User.Name,
                        Surname = task.User.Surname,
                        Username = task.User.Username,
                        Role = task.User.Role
                    } : null
                }).ToList() ?? new List<TaskServiceInfo>(),

                UserInfo = s.User != null ? new UserInfo
                {
                    Id = s.User.Id,
                    Name = s.User.Name,
                    Surname = s.User.Surname,
                    Username = s.User.Username,
                    Role = s.User.Role
                } : null
            });
        }


        //OVO CES MORATI DA MENJAS DA IMAS DETALJE I O TASKOVIMA I RADNICIMA KOJI RADE NA NJIMA + USER KOJI JE POKRENUO SERVICE-UserInfo
        //Stavi da umesto service-a vraca ServiceInfo dto koji ce imati sve ovo. FORMATIRAJ DATUM ZA RESPONSE U ToString("yyyy-MM-dd"),
        public async Task<ServiceInfo> GetServiceById(Guid serviceId)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);
            if (service == null)
            {
                throw new Exception("Service Id doesn't exist");
            }

            var serviceInfo = new ServiceInfo
            {
                Id = service.Id,
                FailureDescription = service.FailureDescription,
                Picture = service.Picture,
                StartDate = service.StartDate.ToString("yyyy-MM-dd"),
                EndDate = service.EndDate?.ToString("yyyy-MM-dd"),
                ServiceStatus = service.ServiceStatus,
                razlogOdbijanja = service.razlogOdbijanja,
                UserInfo = service.User != null ? new UserInfo
                {
                    Id = service.User.Id,
                    Name = service.User.Name,
                    Surname = service.User.Surname,
                    Username = service.User.Username,
                    Role = service.User.Role
                } : null,
                TaskServiceInfo = service.TaskServices?.Select(task => new TaskServiceInfo
                {
                    Id = task.Id,
                    TaskDescription = task.TaskDescription,
                    Status = task.Status,
                    EndDateTask = task.EndDateTask?.ToString("yyyy-MM-dd"),
                    RazlogOdbijanja = task.razlogOdbijanja,
                    WorkerInfo = task.User != null ? new UserInfo
                    {
                        Id = task.User.Id,
                        Name = task.User.Name,
                        Surname = task.User.Surname,
                        Username = task.User.Username,
                        Role = task.User.Role
                    } : null,
                    SparePartInfo = task.SpareParts?.Select(sp => new SparePartInfo
                    {
                        Name = sp.Name,
                        Amount = sp.Amount,
                        IsSpartPartUsed = sp.IsSpartPartUsed
                    }).ToList() ?? new List<SparePartInfo>()
                }).ToList() ?? new List<TaskServiceInfo>()
            };

            return serviceInfo;
        }




        public async Task<UserServiceInfo> CreateService(Guid userId, CreateServiceDto dto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new ArgumentException("User doesn't exist");
            }

            if (!DateTime.TryParseExact(dto.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                throw new FormatException("Invalid Starting Date format. Use yyyy-MM-dd.");
            }

            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);

            var createdService = new Service
            {
                Id = Guid.NewGuid(),
                Picture = dto.Picture,
                FailureDescription = dto.FailureDescription,
                StartDate = startDate,
                ServiceStatus = ServiceStatus.NA_CEKANJU,
                UserId = userId
            };

            await _serviceRepository.CreateService(createdService);

            return new UserServiceInfo
            {
                Id = createdService.Id,
                FailureDescription = createdService.FailureDescription,
                Picture = createdService.Picture,
                StartDate = createdService.StartDate.ToString("yyyy-MM-dd"),
                ServiceStatus = createdService.ServiceStatus
            };
        }

        public async Task AcceptService(Guid serviceId)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);
            if (service == null) throw new KeyNotFoundException("Service not found");
            service.ServiceStatus = ServiceStatus.U_TOKU;
            await _serviceRepository.Update(service);
        }

        public async Task DeclineService(Guid serviceId, DeclineServiceDto dto)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);
            if (service == null) 
            {
                throw new KeyNotFoundException("Service not found");
            }
            
            service.razlogOdbijanja = dto.razlogOdbijanja;
            service.ServiceStatus = ServiceStatus.ODBIJEN;
            service.EndDate = DateTime.UtcNow;

            //ako je otkazan brisi sve njegove taskove
            foreach( var task in service.TaskServices)
            {
                await _tasRepository.DeleteTask(task.Id);
            }

            await _serviceRepository.Update(service);
        }

        public async Task DeleteService(Guid serviceId)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);

            if (service == null)
            {
                throw new ArgumentException("Service not found");
            }

            var timeUntilStart = service.StartDate - DateTime.UtcNow;
            if (timeUntilStart < TimeSpan.FromHours(24))
            {
                throw new InvalidOperationException("Service can't be canceled <24h before starting");
            }

            await _serviceRepository.DeleteService(serviceId);
        }

    }
}
