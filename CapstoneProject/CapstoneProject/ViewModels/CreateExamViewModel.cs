using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.ViewModels
{
    public class CreateExamViewModel
    {
        public List<string> QuestionType { get; set; }



        public CreateExamViewModel()
        {
            

            QuestionType = App.AcademicRepo.GetQuestionTypeList();


        }
    }
}
