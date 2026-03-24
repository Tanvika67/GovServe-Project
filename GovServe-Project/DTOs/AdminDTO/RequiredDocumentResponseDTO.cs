namespace GovServe_Project.DTOs.Admin
{
    public class RequiredDocumentResponseDTO
    {
        public int DocumentID { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string Mandatory { get; set; } = string.Empty;
    }
}