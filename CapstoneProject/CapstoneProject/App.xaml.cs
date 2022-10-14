using CapstoneProject.Database;
using CapstoneProject.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CapstoneProject
{
    public partial class App : Application
    {
        public static AcademicTrackerRepository AcademicRepo { get; private set; }
        public static Page page { get; private set; }
        

        public static string dbpath;
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        public App(string dbPath)
        {
            InitializeComponent();

            AcademicRepo = new AcademicTrackerRepository(dbPath);
            MainPage = new NavigationPage(new LoginPage());           
            page = MainPage;     
            dbpath = dbPath;


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
