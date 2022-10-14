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
    public partial class ReportPage : ContentPage
    {
        public ReportViewModel viewModel;
        public ReportPage()
        {
            InitializeComponent();

            viewModel = new ReportViewModel();
            BindingContext = viewModel;
            
           


        }

        

    }
}