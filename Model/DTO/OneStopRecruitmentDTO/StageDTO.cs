using Model.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsStage")]
    public class StageDTO : BaseModel
    {
        [Key]
        [Column("IDStage")]
        public int IDStage { get; set; }
        [Column("StageName")]
        public string StageName { get; set; }
        [Column("StageLevel")]
        public int StageLevel { get; set; }
    }
}
