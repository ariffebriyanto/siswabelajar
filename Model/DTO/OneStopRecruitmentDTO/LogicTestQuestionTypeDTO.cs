using Model.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsLogicTestQuestionType")]
    public class LogicTestQuestionTypeDTO : BaseModel
    {
        [Key]
        [Column("IDLogicTestQuestionType")]
        public int IDLogicTestQuestionType { get; set; }

        [Column("LogicTestQuestionType")]
        public string LogicTestQuestionType { get; set; }
    }
}
