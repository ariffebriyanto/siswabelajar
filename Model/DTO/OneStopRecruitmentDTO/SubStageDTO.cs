using Model.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsSubStage")]
    public class SubStageDTO : BaseModel
    {
        [Key]
        [Column("IDSubStage")]
        public int IDSubStage { get; set; }
        [Column("IDStage")]
        public int IDStage { get; set; }
        [Column("SubStageName")]
        public string SubStageName { get; set; }
    }
}
