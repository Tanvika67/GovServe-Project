namespace GovServe_Project.DTOs.Admin
{
    public class EligibilityRuleResponseDTO
    {
        public int RuleID { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string RuleDescription { get; set; } = string.Empty;
        public string RuleExpression { get; set; } = string.Empty;
    }
}
