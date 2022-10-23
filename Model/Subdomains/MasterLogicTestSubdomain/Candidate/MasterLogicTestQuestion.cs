using Microsoft.AspNetCore.Http;
using System;

namespace Model.Subdomains.MasterLogicTestSubdomain.Candidate
{
    public class MasterLogicTestQuestion
    {
        public Guid IDLogicTestQuestion { get; set; }        
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

        public string Answer { get; set; }
        public bool IsPicked { get; set; }
    }
}
