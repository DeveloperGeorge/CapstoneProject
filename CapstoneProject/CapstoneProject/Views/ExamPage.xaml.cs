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
    public partial class ExamPage : ContentPage
    {
        public ExamViewModel viewModel;
        public NotificationId NotificationsId { get; set; }
        public int NewId;
        public List<NotificationId> NotificationList { get; set; }

        public ExamPage()
        {
            InitializeComponent();
        }

        public ExamPage(ExamViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
        }

        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.UpdateObj.Execute(null);
            DisplayAlert("Update Info", App.AcademicRepo.statusMessage, "OK");
        }

        private void DeleteExam_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.DeleteObj.Execute(null);
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
            CrossLocalNotifications.Current.Show(viewModel.Exa.AssessmentName, "This Assessment ends on " + viewModel.Exa.End.ToShortDateString() + "", NewId, DueDateNotification_Picker.Date);
            App.AcademicRepo.AddNewIdFromExam(viewModel.Exa.CourseId, viewModel.Exa.TermId, viewModel.Exa.ExamId);
        }
    }
}