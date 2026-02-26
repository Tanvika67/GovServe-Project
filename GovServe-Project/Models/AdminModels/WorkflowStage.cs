using GovServe_Project.Models.AdminModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("WorkflowStages")]
public class WorkflowStage
{
    [Key]
    public int StageID { get; set; }

    [Required]
    public int ServiceID { get; set; }

    [Required]
    public string ResponsibleRole { get; set; } = string.Empty;

    [Required]
    public int SequenceNumber { get; set; }

    public int SLA_Days { get; set; }

    [ForeignKey(nameof(ServiceID))]
    public Service? Service { get; set; }
}