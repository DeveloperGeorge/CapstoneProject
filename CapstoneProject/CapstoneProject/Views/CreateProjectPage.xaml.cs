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
    public partial class CreateProjectPage : ContentPage
    {
        CreateProjectViewModel viewModel;
        public int tempCourseId;
        public int tempTermId;
        public CreateProjectPage(int tempCId, int tempTId)
        {
            InitializeComponent();
            tempCourseId = tempCId;
            tempTermId = tempTId;
            viewModel = new CreateProjectViewModel();
            BindingContext = viewModel;
        }

        private void cancelAddProject_Clicked(object sender, EventArgs e)
        {
            App.page.Navigation.PopModalAsync();
        }

        private void saveAddProject_Clicked(object sender, EventArgs e)
        {

            if (ProjectTypePicker.SelectedItem == null)
            {
                errorlabel1.Text = "Please select a project type";
            }
            else if (ParticipantCountPicker.SelectedItem == null)
            {
                errorlabel2.Text = "Please select the number of participants";
            }
            else 
            {
                App.AcademicRepo.AddNewProject(tempTermId, tempCourseId, ProjectName_Entry.Text, ProjectStart_Picker.Date, ProjectEnd_Picker.Date, ProjectDes_Editor.Text, ProjectTypePicker.SelectedItem.ToString(), ParticipantCountPicker.SelectedItem.ToString());
                if (App.AcademicRepo.projAdded == true)
                {
                    App.page.Navigation.PopModalAsync();
                }
                else if (App.AcademicRepo.projAdded == false)
                {
                    DisplayAlert("Failed to add new Assessment", App.AcademicRepo.statusMessage, "OK");
                }
            }
        }
    }
}