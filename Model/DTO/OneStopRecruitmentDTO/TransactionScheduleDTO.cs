using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrSchedule")]
    public class TransactionScheduleDTO : BaseModel
    {
        [Key]
        [Column("IDMappingSchedule")]
        public Guid IDMappingSchedule { get; set; }

        [Column("IDSchedule")]
        public Guid IDSchedule { get; set; }

        [Column("IDUser")]
        public Guid IDUser { get; set; }
    }
}
