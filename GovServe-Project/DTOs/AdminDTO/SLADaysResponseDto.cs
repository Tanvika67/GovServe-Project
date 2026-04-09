namespace GovServe_Project.DTOs.AdminDTO
{
    public class SLADayResponseDto
    {
        public int SLADayID { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int Days { get; set; }
    }
}
