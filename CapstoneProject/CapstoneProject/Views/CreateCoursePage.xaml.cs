using CapstoneProject.Models;
using CapstoneProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCoursePage : ContentPage
    {
        CreateCourseViewModel viewModel;

        public Course course { get; set; }
        public int tempTermId;

        public CreateCoursePage(int tempId)
        {
            InitializeComponent();
            viewModel = new CreateCourseViewModel();
            BindingContext = viewModel;
            tempTermId = tempId;
        }

        private void cancelAddCourse_Clicked(object sender, EventArgs e)
        {
            App.page.Navigation.PopModalAsync();
        }

        private void saveAddCourse_Clicked(object sender, EventArgs e)
        {
            if (CourseStatusPicker.SelectedItem == null)
            {
                errorlabel.Text = "Failed to add " + addCourseTitle.Text + ". Error: Must select course status";
            }
            else
            {
                App.AcademicRepo.AddNewCourse(tempTermId, addCourseTitle.Text, CourseStartDate.Date, CourseEndDate.Date, CourseStatusPicker.SelectedItem.ToString(), InstructorName.Text, instructorNum.Text, instructorEmail.Text);
                if (App.AcademicRepo.courseAdded == true)
                {
                    App.page.Navigation.PopModalAsync();
                }
                errorlabel.Text = App.AcademicRepo.statusMessage;
            }
        }
    }
}