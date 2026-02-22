namespace GovServe_Project.DTOs.Admin
{
    public class EligibilityRuleDTO
    {
        public int ServiceID { get; set; }
        public string RuleDescription { get; set; } = string.Empty;
        public string RuleExpression { get; set; } = string.Empty;
    }
}

