using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe_Project.Enum;

namespace GovServe_Project.Models.AdminModels
{
    [Table("SLARecords")]
    public class SLARecord
    {
        [Key]
        public int SLARecordID { get; set; }

        [Required]
        public int CaseID { get; set; } 

        [Required]
        public int StageID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SLAStatus Status { get; set; }

        [ForeignKey(nameof(StageID))]
        public WorkflowStage? Stage { get; set; }

        [ForeignKey(nameof(CaseID))]
        public Case? Case { get; set; }
    }

}