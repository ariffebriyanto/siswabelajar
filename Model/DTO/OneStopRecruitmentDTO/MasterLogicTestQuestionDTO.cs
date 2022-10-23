using Model.DTO.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DTO.OneStopRecruitmentDTO
{
    [Table("MsLogicTestQuestion")]
    public class MasterLogicTestQuestionDTO : BaseModel
    {
        [Key]
        [Column("IDLogicTestQuestion")]
        public Guid IDLogicTestQuestion { get; set; }

        [Column("IDLogicTestQuestionType")]
        public int IDLogicTestQuestionType { get; set; }

        [Column("QuestionText")]
        public string QuestionText { get; set; }

        [Column("QuestionImage")]
        public string QuestionImage { get; set; }

        [Column("FirstChoiceText")]
        public string FirstChoiceText { get; set; }

        [Column("FirstChoiceImage")]
        public string FirstChoiceImage { get; set; }

        [Column("SecondChoiceText")]
        public string SecondChoiceText { get; set; }

        [Column("SecondChoiceImage")]
        public string SecondChoiceImage { get; set; }

        [Column("ThirdChoiceText")]
        public string ThirdChoiceText { get; set; }

        [Column("ThirdChoiceImage")]
        public string ThirdChoiceImage { get; set; }

        [Column("FourthChoiceText")]
        public string FourthChoiceText { get; set; }

        [Column("FourthChoiceImage")]
        public string FourthChoiceImage { get; set; }

        [Column("CorrectChoice")]
        public string CorrectChoice { get; set; }        
    }
}
