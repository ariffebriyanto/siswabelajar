using Model.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsMinimumScore")]
    public class MinimumScoreDTO : BaseModel
    {
        [Key]
        [Column("IDMinimumScore")]
        public int IDMinimumScore { get; set; }

        [Column("IDPeriod")]
        public int IDPeriod { get; set; }

        [Column("IDStage")]
        public int IDStage { get; set; }

        [Column("IDSubStage")]
        public int IDSubStage { get; set; }

        [Column("MinimumScore")]
        public decimal MinimumScore { get; set; }
    }
}
