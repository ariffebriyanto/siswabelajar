using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsCandidate")]
    public class CandidateDTO : BaseModel
    {
        [Key]
        [Column("IDCandidate")]
        public Guid IDCandidate { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDRole")]
        public int IDRole { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("IDPosition")]
        public string IDPosition { get; set; }

        [Column("NIM")]
        public string NIM{ get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("IsAccepted")]
        public int IsAccepted { get; set; }
    }
}
