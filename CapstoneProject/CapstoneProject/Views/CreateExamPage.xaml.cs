using CapstoneProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateExamPage : ContentPage
    {

        CreateExamViewModel viewModel;

        public int tempCourseId;
        public int tempTermId;
        

        public CreateExamPage(int tempCId, int tempTId)
        {
            InitializeComponent();
            tempCourseId = tempCId;
            tempTermId = tempTId;
            viewModel = new CreateExamViewModel();
            BindingContext = viewModel;
        }

        private void cancelAddExam_Clicked(object sender, EventArgs e)
        {
            App.page.Navigation.PopModalAsync();
        }

        private void saveAddExam_Clicked(object sender, EventArgs e)
        {
            if (QuestionTypePicker.SelectedItem == null)
            {
                errorlabel.Text = "Please select a question type";
            }
            else
            {
                App.AcademicRepo.AddNewExam(tempTermId, tempCourseId, ExamName_Entry.Text, ExamStart_Picker.Date, ExamEnd_Picker.Date, ExamDes_Editor.Text, QuestionTypePicker.SelectedItem.ToString(), QuestionNumEntry.Text);
                if (App.AcademicRepo.exAdded == true)
                {
                    App.page.Navigation.PopModalAsync();
                }
                else if (App.AcademicRepo.exAdded == false)
                {
                    DisplayAlert("Failed to add new Assessment", App.AcademicRepo.statusMessage, "OK");
                }
            }
        }
    }
}