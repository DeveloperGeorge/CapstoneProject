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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async private void Login_button_Clicked(object sender, EventArgs e)
        {
            if (App.AcademicRepo.CheckLogin(UserNameEntry.Text, PasswordEntry.Text) == true)
            {

                await App.page.Navigation.PushModalAsync(new NavigationPage(new TermsPage()));
                
                
            }
            else
            {
                await DisplayAlert("Login Failed", "User name and password did not match", "Ok");
            }
        }
    }
}