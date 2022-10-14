using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CapstoneProject.ViewModels
{
    public class ExamViewModel
    {
        public Exam Exa { get; set; }
        public Command UpdateObj { get; set; }
        public Command DeleteObj { get; set; }
        public List<string> QuestionType { get; set; }

        public ExamViewModel(Exam exa = null)
        {

            Exa = exa ?? new Exam();
            UpdateObj = new Command(ExecuteUpdate);
            DeleteObj = new Command(ExecuteDelete);

            QuestionType = App.AcademicRepo.GetQuestionTypeList();

        }

        public void ExecuteUpdate()
        {
            Exam exa = Exa;
            App.AcademicRepo.UpdateExam(exa);
        }

        public void ExecuteDelete()
        {
            Exam exa = Exa;
            int ExaId = exa.ExamId;
            App.AcademicRepo.RemoveExam(ExaId);
            //App.page.Navigation.PopAsync();

        }
    }
}
