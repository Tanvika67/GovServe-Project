using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;
using GovServe_Project.Models.SuperModels;

namespace GovServe_Project.Models.AdminModels
{
    [Table("SLARecords")]
    public class SLARecords
    {
        [Key]
        public int SLARecordID { get; set; }

        [Required]
        public int CaseId { get; set; } 

        [Required]
        public int StageID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SLAStatus Status { get; set; }

        [ForeignKey(nameof(StageID))]
        public WorkflowStage? Stage { get; set; }

        [ForeignKey(nameof(CaseId))]
        public Case? Case { get; set; }
    }

}