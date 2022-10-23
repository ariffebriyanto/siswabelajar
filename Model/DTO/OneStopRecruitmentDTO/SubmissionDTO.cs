using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("TrSubmission")]
    public class SubmissionDTO : BaseModel
    {
        [Key]
        [Column("IDSubmission")]
        public Guid IDSubmission { get; set; }

        [Column("IDAssignment")]
        public int IDAssignment { get; set; }

        [Column("IDUser")]
        public Guid IDUser { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }

        [Column("FilePath")]
        public string FilePath { get; set; }
    }
}
