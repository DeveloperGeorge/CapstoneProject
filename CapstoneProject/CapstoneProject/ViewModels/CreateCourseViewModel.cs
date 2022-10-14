using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.ViewModels
{
    public class CreateCourseViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> CourseStatus { get; set; }



        public CreateCourseViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);

            CourseStatus = App.AcademicRepo.GetCourseStatusList();


        }
    }
}
