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
    public partial class TermsPage : ContentPage
    {

        TermsViewModel viewModel;

        public TermsPage()
        {
            InitializeComponent();
            viewModel = new TermsViewModel();
            BindingContext = viewModel;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.TermsListView.ItemsSource = this.viewModel.LoadTermList();
            this.CurrentAssessmentCount.Text = this.viewModel.GetAssessmentCount();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.viewModel.TermsList.Clear();
            this.viewModel.termsList.Clear();
        }



        async private void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateTermPage()));
        }

        private void TermSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var TermSearch = TermSearchBar.Text;
            this.TermsListView.ItemsSource = this.viewModel.LoadSearchTermList(TermSearch);
        }

        private void ResetSearch_button_Clicked(object sender, EventArgs e)
        {
            this.TermsListView.ItemsSource = this.viewModel.LoadTermList();
        }

        async private void GotoReport_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportPage());
        }

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var term = (Term)layout.BindingContext;
            await Navigation.PushAsync(new TermDetailPage(new TermDetailViewModel(term)));
        }

        private void TermSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TermsListView.ItemsSource = this.viewModel.LoadTermList();
        }
    }
}