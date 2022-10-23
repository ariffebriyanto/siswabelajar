using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsScoringComponentType")]
    public class ScoringComponentTypeDTO : BaseModel
    {
        [Key]
        [Column("IDScoringComponentType")]
        public Guid IDScoringComponentType { get; set; }

        [Column("ScoringComponentType")]
        public string ScoringComponentType { get; set; }
    }
}
