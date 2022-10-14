using CapstoneProject.Models;
using CapstoneProject.ViewModels;
using Plugin.LocalNotifications;
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
    public partial class ProjectPage : ContentPage
    {

        public ProjectViewModel viewModel;
        public NotificationId NotificationsId { get; set; }
        public int NewId;
        public List<NotificationId> NotificationList { get; set; }

        public ProjectPage()
        {
            InitializeComponent();
        }

        public ProjectPage(ProjectViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.UpdatePer.Execute(null);
            DisplayAlert("Update Info", App.AcademicRepo.statusMessage, "OK");
        }

        private void DeleteProject_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.DeletePer.Execute(null);
            Navigation.PopAsync();
        }

        private void AddDueDateNotification_Button_Clicked(object sender, EventArgs e)
        {
            NotificationList = App.AcademicRepo.GetNotificationId();
            if (NotificationList.Count == 0)
            {
                NewId = 1;
            }
            else
            {
                NotificationsId = NotificationList.Last<NotificationId>();
                NewId = NotificationsId.NotifyId + 1;
            }
            CrossLocalNotifications.Current.Show(viewModel.Proj.AssessmentName, "This Assessment ends on " + viewModel.Proj.End.ToShortDateString() + "", NewId, DueDateNotification_Picker.Date);
            App.AcademicRepo.AddNewIdFromProject(viewModel.Proj.CourseId, viewModel.Proj.TermId, viewModel.Proj.ProjectId);
        }
    }
}