using CapstoneProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionalNotePage : ContentPage
    {

        public OptionalNotesViewModel viewModel;

        public OptionalNotePage()
        {
            InitializeComponent();
            viewModel = new OptionalNotesViewModel();
            BindingContext = viewModel;
        }

        public OptionalNotePage(OptionalNotesViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.UpdateNote.Execute(null);
            DisplayAlert("Update Info", App.AcademicRepo.statusMessage, "Ok");
        }

        private void DeleteNote_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.DeleteNote.Execute(null);
            Navigation.PopAsync();
        }

        async private void ShareNote_Button_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = NoteDes_Editor.Text,
                Title = NoteName_Entry.Text,
                Subject = NoteName_Entry.Text,

            });
        }
    }
}