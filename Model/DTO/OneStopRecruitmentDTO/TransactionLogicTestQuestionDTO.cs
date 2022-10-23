using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrLogicTestQuestion")]
    public class TransactionLogicTestQuestionDTO : BaseModel
    {
        [Key]
        [Column("IDMappingQuestion")]
        public Guid IDMappingQuestion { get; set; }

        [Column("IDLogicTestQuestion")]
        public Guid IDLogicTestQuestion { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }
    }
}
