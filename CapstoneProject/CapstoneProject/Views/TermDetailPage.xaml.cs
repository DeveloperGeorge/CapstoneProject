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
    public partial class TermDetailPage : ContentPage
    {

        TermDetailViewModel viewModel;
        public int tempId;

        public TermDetailPage()
        {
            InitializeComponent();
            var term = new Term
            {
                TermName = "Temp Term",
                TermStart = DateTime.Now,
                TermEnd = DateTime.Now.AddDays(1)
            };


            viewModel = new TermDetailViewModel(term);
            BindingContext = viewModel;

        }

        public TermDetailPage(TermDetailViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            tempId = this.viewModel.Term.TermId;

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.CourseCollectionView.ItemsSource = this.viewModel.LoadCourseList();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.viewModel.CourseList.Clear();
        }


        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.UpdateTerm.Execute(null);
            DisplayAlert("Update Info", App.AcademicRepo.statusMessage.ToString(), "OK");
        }

        private void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            viewModel.DeleteTerm.Execute(null);
            Navigation.PopAsync();
        }


        async private void AddCourseButton_Clicked(object sender, EventArgs e)
        {
            if (this.viewModel.CourseList.Count < 6)
            {
                await Navigation.PushModalAsync(new NavigationPage(new CreateCoursePage(tempId)));
            }
            else
            {
                await DisplayAlert("Page Error", "Maximum course limit reached", "Ok");
            }
        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var course = (Course)layout.BindingContext;
            await Navigation.PushAsync(new CoursePage(new CourseViewModel(course)));
        }

       
    }
}