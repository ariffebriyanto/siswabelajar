using Model.DBConstraint;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MasterLogicTestSubdomain
{
    public class LogicTestQuestionType
    {
        public int IDLogicTestQuestionType { get; set; }
        [Required(ErrorMessage = AlertConstraint.LogicTest.EmptyQuestionType)]
        public string QuestionType { get; set; }
    }
}
