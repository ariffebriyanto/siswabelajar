using Model.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsPosition")]
    public class PositionDTO : BaseModel
    {
        [Key]
        [Column("IDPosition")]
        public string IDPosition { get; set; }
        [Column("PositionName")]
        public string PositionName { get; set; }
    }
}
