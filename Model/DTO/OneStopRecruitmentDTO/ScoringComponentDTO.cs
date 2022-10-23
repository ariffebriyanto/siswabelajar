using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsScoringComponent")]
    public class ScoringComponentDTO : BaseModel
    {
        [Key]
        [Column("IDScoringComponent")]
        public Guid IDScoringComponent { get; set; }

        [Column("IDScoringComponentType")]
        public Guid IDScoringComponentType { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("IDSubStage")]
        public int IDSubStage { get; set; }

        [Column("IDPosition")]
        public string IDPosition { get; set; }

        [Column("ScoringComponent")]
        public string ScoringComponent { get; set; }

        [Column("MinScore")]
        public decimal MinScore { get; set; }

        [Column("MaxScore")]
        public decimal MaxScore { get; set; }
    }
}
