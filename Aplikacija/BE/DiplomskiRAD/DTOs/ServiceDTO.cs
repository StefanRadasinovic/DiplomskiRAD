using DiplomskiRAD.Enums;
using DiplomskiRAD.Models;
using System.ComponentModel.DataAnnotations.Schema;
using static DiplomskiRAD.DTOs.UserDTO;

namespace DiplomskiRAD.DTOs
{
    public class ServiceDTO
    {
       
        public class UserServiceInfo
        {
            public Guid Id { get; set; }

            public string FailureDescription { get; set; }

            public string? Picture { get; set; }

            public string StartDate { get; set; } //datum pospeca zahteva 

            public string? EndDate { get; set; }

            public ServiceStatus ServiceStatus { get; set; }

            public string? razlogOdbijanja { get; set; }

        }


        public class DirektorServiceInfo
        {
            public Guid Id { get; set; }

            public string FailureDescription { get; set; }

            public string? Picture { get; set; }

            public string StartDate { get; set; } //datum pospeca zahteva 

            public string? EndDate { get; set; }

            public ServiceStatus ServiceStatus { get; set; }

            public string? razlogOdbijanja { get; set; }

            public UserInfo UserInfo { get; set; }

        }

        public class CreateServiceDto
        {
            public string? Picture { get; set; }
            public string FailureDescription { get; set; }

            public string StartDate { get; set; } //datum pospeca zahteva-tj ovde kada ga je poslao

        }

        public class DeleteServiceDto
        {
            public string razlogOdbijanja { get; set; }

        }
    }
}
