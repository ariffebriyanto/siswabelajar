using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsPeriod")]
    public class PeriodDTO : BaseModel
    {
        [Key]
        [Column("IDPeriod")]
        public int IDPeriod { get; set; }
        [Column("IDStage")]
        public int IDStage { get; set; }
        [Column("PeriodName")]
        public string PeriodName { get; set; }

        [Column("IsActive")]
        public int IsActive{ get; set; }

        [Column("DeadlineStart")]
        public DateTime DeadlineStart { get; set; }
        [Column("DeadlineEnd")]
        public DateTime DeadlineEnd { get; set; }

        [Column("IsComplete")]
        public int IsComplete { get; set; }
    }
}
