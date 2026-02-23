using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.Admin
{
    public class DepartmentDTO
    {
        public string DepartmentName { get; set; } = default!;
        public string? Description { get; set; }
        public DepartmentStatus Status { get; set; }
    }
}