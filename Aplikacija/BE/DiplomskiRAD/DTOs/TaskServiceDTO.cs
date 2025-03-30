using DiplomskiRAD.Enums;
using Microsoft.AspNetCore.Identity;
using static DiplomskiRAD.DTOs.SparePartDTO;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.DTOs
{
    public class TaskServiceDTO
    {
        public class TaskServiceInfo
        {
            public Guid Id { get; set; }
            public string TaskDescription { get; set; }
            public string? EndDateTask { get; set; }
            public ServiceStatus Status { get; set; }
            public string? RazlogOdbijanja { get; set; }
            public List<SparePartInfo> SparePartInfo { get; set; }

            public UserInfo WorkerInfo { get; set; } //radnik

            public Guid ServiceId { get; set; }
        }

        public class CreateTaskDto
        {
            public string TaskDescription { get; set; }
        }

        public class RejectTaskDto
        {
            public string? RazlogOdbijanja { get; set; }
        }
    }
}
