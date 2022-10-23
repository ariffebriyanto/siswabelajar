using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsSchedule")]
    public class MasterScheduleDTO : BaseModel
    {
        [Key]
        [Column("IDSchedule")]
        public Guid IDSchedule { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("IDSubStage")]
        public int IDSubStage { get; set; }

        [Column("IDPosition")]
        public string IDPosition { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("StartTime")]
        public TimeSpan StartTime { get; set; }

        [Column("EndTime")]
        public TimeSpan EndTime { get; set; }

        [Column("Room")]
        public string Room { get; set; }

        [Column("Limit")]
        public int Limit { get; set; }

        [Column("IDReviewer")]
        public Guid? IDReviewer { get; set; }
    }
}
