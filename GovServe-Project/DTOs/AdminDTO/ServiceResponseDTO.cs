using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.Admin
{
    public class ServiceResponseDTO
    {
        public int ServiceID { get; set; }
        public int DepartmentID { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int SLA_Days { get; set; }
         public ServiceStatus Status { get; set; } 

       

    }
}
