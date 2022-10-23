using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrCandidateScore")]
    public class CandidateScoreDTO : BaseModel
    {
        [Key]
        [Column("IDCandidateScore")]
        public Guid IDCandidateScore { get; set; }

        [Column("IDCandidate")]
        public Guid IDCandidate { get; set; }

        [Column("IDScoringComponent")]
        public Guid IDScoringComponent { get; set; }

        [Column("Score")]
        public decimal Score { get; set; }

        [Column("Note")]
        public string Note { get; set; }
    }
}
