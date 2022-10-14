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
    public partial class CreateTermPage : ContentPage
    {

        CreateTermPageViewModel viewModel;
        public Term term { get; set; }

        public CreateTermPage()
        {
            InitializeComponent();

            viewModel = new CreateTermPageViewModel();
            BindingContext = viewModel;

        }

        private void cancelAddTerm_Clicked(object sender, EventArgs e)
        {
            App.page.Navigation.PopModalAsync();
        }

        private void saveTerm_Clicked(object sender, EventArgs e)
        {
            App.AcademicRepo.AddNewTerm(addTermTitle.Text, StartDatePicker.Date, EndDatePicker.Date);
            if (App.AcademicRepo.termAdded == true)
            {

                App.page.Navigation.PopModalAsync();
            }
            errorlabel.Text = App.AcademicRepo.statusMessage;
        }
    }
}