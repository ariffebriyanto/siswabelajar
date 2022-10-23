using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsAssignment")]
    public class AssignmentDTO : BaseModel
    {
        [Key]
        [Column("IDAssignment")]
        public int IDAssignment { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("IDSubStage")]
        public int IDSubStage { get; set; }

        [Column("DeadlineStart")]
        public DateTime DeadlineStart { get; set; }

        [Column("DeadlineEnd")]
        public DateTime DeadlineEnd { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        [Column("FilePath")]
        public string FilePath { get; set; }
    }
}
