using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.AdminDTO
{
   
        public class DepartmentResponseDTO
        {
            public int DepartmentID { get; set; }
            public string DepartmentName { get; set; } = default!;
            public string? Description { get; set; }
            public DepartmentStatus Status { get; set; }
        }
    }

