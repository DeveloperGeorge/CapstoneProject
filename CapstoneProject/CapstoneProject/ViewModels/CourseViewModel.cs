using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CapstoneProject.ViewModels
{
    public class CourseViewModel
    {
        public Course Course { get; set; }
        public List<string> CourseStatus { get; set; }
        public List<Exam> ExamList { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<Note> NoteList { get; set; }

        public Command UpdateCourse { get; set; }
        public Command DeleteCourse { get; set; }


        public CourseViewModel(Course course = null)
        {

            Course = course ?? new Course();
            CourseStatus = App.AcademicRepo.GetCourseStatusList();
            ExamList = App.AcademicRepo.GetAllExams(Course.CourseId);
            ProjectList = App.AcademicRepo.GetAllProjects(Course.CourseId);
            NoteList = App.AcademicRepo.GetAllNotes(Course.CourseId);

            UpdateCourse = new Command(ExecuteUpdate);
            DeleteCourse = new Command(ExecuteDelete);



        }



        public List<Exam> LoadObjList()
        {
            ExamList = App.AcademicRepo.GetAllExams(Course.CourseId);
            return ExamList;
        }

        public List<Project> LoadPerList()
        {
            ProjectList = App.AcademicRepo.GetAllProjects(Course.CourseId);
            return ProjectList;
        }


        public List<Note> LoadNoteList()
        {
            NoteList = App.AcademicRepo.GetAllNotes(Course.CourseId);
            return NoteList;
        }

        public void ExecuteUpdate()
        {
            Course course = Course;
            App.AcademicRepo.UpdateCourse(course);
        }

        public void ExecuteDelete()
        {
            Course course = this.Course;
            int courseId = course.CourseId;
            App.AcademicRepo.RemoveCourse(courseId);
            //App.page.Navigation.PopAsync();

        }

    }
}
