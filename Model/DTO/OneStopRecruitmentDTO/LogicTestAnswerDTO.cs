using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrLogicTestAnswer")]
    public class LogicTestAnswerDTO : BaseModel
    {
        [Key]
        [Column("IDLogicTestAnswer")]
        public Guid IDLogicTestAnswer { get; set; }

        [Column("IDLogicTestQuestion")]
        public Guid IDLogicTestQuestion { get; set; }

        [Column("IDCandidate")]
        public Guid IDCandidate { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("Answer")]
        public string Answer { get; set; }
    }
}
