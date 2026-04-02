public class CaseResponseDto
{
    public int CaseId { get; set; }
    public string ApplicationNumber { get; set; } = "";
    public string ServiceName { get; set; } = "";
    public string DepartmentName { get; set; } = "";
    public string OfficerName { get; set; } = "";
    public string OfficerDepartment { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime LastUpdated { get; set; }
}