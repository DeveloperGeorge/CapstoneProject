using CapstoneProject.Models;
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
    public partial class CreateNotePage : ContentPage
    {

        public Note note;
        public int tempCourseId;
        public int tempTermId;

        public CreateNotePage(int tempCId, int tempTId)
        {
            InitializeComponent();
            tempCourseId = tempCId;
            tempTermId = tempTId;
        }

        private void cancelAddNote_Clicked(object sender, EventArgs e)
        {
            //App.page.Navigation.PopModalAsync();
            Navigation.PopModalAsync();
        }

        private void saveAddNote_Clicked(object sender, EventArgs e)
        {
            App.AcademicRepo.AddNewNote(tempTermId, tempCourseId, NoteNameEntry.Text, NoteDescriptionEditor.Text);
            if (App.AcademicRepo.noteAdded == true)
            {
                //App.page.Navigation.PopModalAsync();
                Navigation.PopModalAsync();
            }
            else if (App.AcademicRepo.noteAdded == false)
            {
                DisplayAlert("Failed to add new Note", App.AcademicRepo.statusMessage, "Ok");
            }
        }
    }
}