using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CapstoneProject.ViewModels
{
    public class TermDetailViewModel
    {
        public Term Term { get; set; }
        public List<Course> CourseList { get; set; }

        public Command UpdateTerm { get; set; }
        public Command DeleteTerm { get; set; }



        public TermDetailViewModel(Term term = null)
        {

            Term = term ?? new Term();

            UpdateTerm = new Command(ExecuteUpdate);
            DeleteTerm = new Command(ExecuteDelete);

            CourseList = App.AcademicRepo.GetAllCourses(Term.TermId);

        }

        public List<Course> LoadCourseList()
        {
            CourseList = App.AcademicRepo.GetAllCourses(Term.TermId);
            return CourseList;
        }

        public void ExecuteUpdate()
        {
            Term term = Term;


            App.AcademicRepo.UpdateTerm(term);
        }

        public void ExecuteDelete()
        {
            Term term = this.Term;
            int termId = term.TermId;
            App.AcademicRepo.RemoveTerm(termId);
            //App.page.Navigation.PopAsync();
            
            

        }
    }
}
