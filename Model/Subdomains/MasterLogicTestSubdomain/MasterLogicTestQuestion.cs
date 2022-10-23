using Microsoft.AspNetCore.Http;
using Model.DBConstraint;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Subdomains.MasterLogicTestSubdomain
{
    public class MasterLogicTestQuestion
    {
        public Guid IDLogicTestQuestion { get; set; }        
        [Required(ErrorMessage = AlertConstraint.LogicTest.EmptyQuestionType)]
        [Range(1, Int32.MaxValue, ErrorMessage = AlertConstraint.LogicTest.NotSelectQuestionType)]
        public int IDLogicTestQuestionType { get; set; }
        public string QuestionType { get; set; }
        public string Type { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImage { get; set; }

        public IFormFile QuestionFile { get; set; }

        public string FirstChoiceType { get; set; }
        public string FirstChoiceText { get; set; }
        public string FirstChoiceImage { get; set; }

        public IFormFile FirstChoiceFile { get; set; }

        public string SecondChoiceType { get; set; }
        public string SecondChoiceText { get; set; }
        public string SecondChoiceImage { get; set; }

        public IFormFile SecondChoiceFile { get; set; }

        public string ThirdChoiceType { get; set; }
        public string ThirdChoiceText { get; set; }
        public string ThirdChoiceImage { get; set; }

        public IFormFile ThirdChoiceFile { get; set; }

        public string FourthChoiceType { get; set; }

        public string FourthChoiceText { get; set; }

        public string FourthChoiceImage { get; set; }

        public IFormFile FourthChoiceFile { get; set; }

        public string CorrectChoice { get; set; }
        public bool IsPicked { get; set; }
    }
}
