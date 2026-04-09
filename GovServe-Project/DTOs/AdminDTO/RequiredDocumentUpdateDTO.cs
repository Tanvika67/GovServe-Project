namespace GovServe_Project.DTOs.Admin
{
    public class RequiredDocumentUpdateDTO
    {
        public string DocumentName { get; set; } = string.Empty;
        public bool Mandatory { get; set; }
    }
}