using Model.DTO.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsEmailTemplate")]
    public class EmailTemplateDTO : BaseModel
    {
        [Key]
        [Column("IDEmailTemplate")]
        public Guid IDEmailTemplate { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("Template")]
        public string Template { get; set; }
    }
}
