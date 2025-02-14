using System.Threading.Tasks;
using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.OrderDTO;
using static DiplomskiRAD.DTOs.ServiceDTO;
using static DiplomskiRAD.DTOs.SparePartDTO;
using static DiplomskiRAD.DTOs.TaskServiceDTO;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.Services
{
    public class TaskServiceService
    {
        private readonly TaskRepository _taskRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly UserRepository _userRepository;
        private readonly SparePartsRepository _sparePartsRepository;

        public TaskServiceService(TaskRepository taskRepository, ServiceRepository serviceRepository, UserRepository userRepository, SparePartsRepository sparePartsRepository) 
        {
            _taskRepository = taskRepository;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
            _sparePartsRepository = sparePartsRepository;
        }

        public async Task<TaskServiceInfo> CreateTask(Guid serviceId, Guid userId, CreateTaskDto dto)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);
            if (service == null)
            {
                throw new Exception("Service not found");
            }

            if(service.ServiceStatus != ServiceStatus.U_TOKU)
            {
                throw new Exception("Service mora biti 'U_TOKU' - prihvacen");
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var newTask = new TaskService
            {
                Id = Guid.NewGuid(),
                TaskDescription = dto.TaskDescription,
                Status = ServiceStatus.NA_CEKANJU,
                ServiceId = serviceId,
                UserId = userId,
            };

            await _taskRepository.CreateTask(newTask);


            var taskInfo = new TaskServiceInfo
            {
                Id = newTask.Id,
                ServiceId= serviceId,
                TaskDescription = newTask.TaskDescription,
                Status = newTask.Status,
                WorkerInfo = new UserInfo //radnika vracas
                {
                    Id = newTask.User.Id,  
                    Name = newTask.User.Name,
                    Surname = newTask.User.Surname,
                    Username = newTask.User.Username,
                    Role = newTask.User.Role
                }
                
            };

            return taskInfo;
        }

        public async Task<TaskServiceInfo> GetTaskById(Guid taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new Exception("Task Id doesn't exist");
            }

            var serviceInfo = new TaskServiceInfo
            {
                Id = task.Id,
                ServiceId = task.ServiceId,
                TaskDescription = task.TaskDescription,
                EndDateTask = task.EndDateTask?.ToString("yyyy-MM-dd"),
                Status = task.Status,
                RazlogOdbijanja = task.razlogOdbijanja,
                WorkerInfo = new UserInfo //radnika vracas
                {
                    Id = task.User.Id,
                    Name = task.User.Name,
                    Surname = task.User.Surname,
                    Username = task.User.Username,
                    Role = task.User.Role
                },
                SparePartInfo = task.SpareParts.Select(sp => new SparePartInfo
                {
                    Id = sp.Id,
                    Name = sp.Name,
                    Amount = sp.Amount,
                    IsSpartPartUsed = sp.IsSpartPartUsed 
                }).ToList()
            };

            return serviceInfo;
        }

        public async Task<List<TaskServiceInfo>> GetTasksForUser(Guid userId)
        {
            var tasks = await _taskRepository.GetTasksByUserId(userId);
            return tasks.Select(t => new TaskServiceInfo
            {
                Id = t.Id,
                TaskDescription = t.TaskDescription,
                EndDateTask = t.EndDateTask?.ToString("yyyy-MM-dd"),
                Status = t.Status,
                RazlogOdbijanja = t.razlogOdbijanja,
                ServiceId = t.ServiceId,
            }).ToList();
        }



        public async Task AcceptTask(Guid taskId, Guid userId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.Status = ServiceStatus.U_TOKU;
            task.UserId = userId;
            await _taskRepository.UpdateTask(task);
        }


        public async Task DeclineTask(Guid taskId, RejectTaskDto dto)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            task.Status = ServiceStatus.ODBIJEN;
            task.razlogOdbijanja = dto.RazlogOdbijanja;
            await _taskRepository.UpdateTask(task);
        }

        public async Task FinishTask(Guid taskId, UsedSparePartDto dto)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
            {
                throw new Exception("Task not found");
            }

            if (task.Status != ServiceStatus.U_TOKU)
            {
                throw new Exception("Task mora biti 'U_TOKU' - prihvacen");
            }


            task.Status = ServiceStatus.ZAVRSEN;
            task.EndDateTask = DateTime.UtcNow;

            
            var sparePart = new SparePart
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Amount = dto.Amount,
                IsSpartPartUsed = IsSpartPartUsed.USED,
                TaskServiceId = task.Id
            };

           await _sparePartsRepository.CreateSparePart(sparePart);
            
            task.SpareParts ??= new List<SparePart>();
            task.SpareParts.Add(sparePart);
            await _taskRepository.UpdateTask(task);


            // ako su svi gotovi servis je isto gotov
            var service = await _serviceRepository.GetServiceById(task.ServiceId);
            if (service.TaskServices.All(t => t.Status == ServiceStatus.ZAVRSEN))
            {
                service.ServiceStatus = ServiceStatus.ZAVRSEN;
                service.EndDate = task.EndDateTask;
                await _serviceRepository.Update(service);
            }
        }


        //OVO TI JE fja ZA PDF
        public async Task<Dictionary<ServiceStatus, List<TaskServiceInfo>>> GetAllTasksForService(Guid serviceId)
        {
            var tasks = await _taskRepository.GetAllTasksForService(serviceId);

            var taskInfos = tasks.Select(t => new TaskServiceInfo
            {
                Id = t.Id,
                TaskDescription = t.TaskDescription,
                EndDateTask = t.EndDateTask?.ToString("yyyy-MM-dd"),
                Status = t.Status,
                RazlogOdbijanja = t.razlogOdbijanja,
                ServiceId = serviceId,
                WorkerInfo = t.User != null ? new UserInfo
                {
                    Id = t.User.Id,
                    Name = t.User.Name,
                    Surname = t.User.Surname,
                    Username = t.User.Username,
                    Role = t.User.Role
                } : null,
                SparePartInfo = t.SpareParts?.Select(sp => new SparePartInfo
                {
                    Id = sp.Id,
                    Name = sp.Name,
                    Amount = sp.Amount,
                    IsSpartPartUsed = sp.IsSpartPartUsed
                }).ToList() ?? new List<SparePartInfo>()
            }).ToList();

            var groupedTasks = taskInfos.GroupBy(t => t.Status)
                                        .ToDictionary(g => g.Key, g => g.ToList());

            return groupedTasks;
        }

        
        public async Task<List<TaskServiceInfo>> GetAllInProgressDeclinedTasks()
        {
            var tasks = await _taskRepository.GetAllInProgressDeclinedTasks();
            return tasks.Select(t => new TaskServiceInfo
            {
                Id = t.Id,
                TaskDescription = t.TaskDescription,
                EndDateTask = t.EndDateTask?.ToString("yyyy-MM-dd"),
                Status = t.Status,
                RazlogOdbijanja = t.razlogOdbijanja
            }).ToList();
        }

    }
}
