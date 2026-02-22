namespace GovServe_Project.DTOs.Admin
{
    public class RequiredDocumentResponseDTO
    {
        public int DocumentID { get; set; }
        public int ServiceID { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public bool Mandatory { get; set; }
    }
}