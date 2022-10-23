using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrCandidateDraft")]
    public class CandidateDraftDTO : BaseModel
    {
        [Key]
        [Column("IDCandidateDraft")]
        public Guid IDCandidateDraft { get; set; }

        [Column("IDCandidate")]
        public Guid IDCandidate { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDPosition")]
        public string IDPosition { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("IsPass")]
        public int IsPass { get; set; }

        [Column("Note")]
        public string Note { get; set; }
    }
}
