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
    public partial class CoursePage : ContentPage
    {
        public CourseViewModel viewModel;
        public int tempTermId;
        public int tempCourseId;
        public int NewId;
        public List<NotificationId> NotificationList { get; set; }
        public NotificationId NotificationsId { get; set; }

        public CoursePage()
        {
            InitializeComponent();

            viewModel = new CourseViewModel();
            BindingContext = viewModel;

        }

        public CoursePage(CourseViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            tempTermId = this.viewModel.Course.TermId;
            tempCourseId = this.viewModel.Course.CourseId;




        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.ExamCollectionView.ItemsSource = this.viewModel.LoadObjList();
            this.ProjectCollectionView.ItemsSource = this.viewModel.LoadPerList();
            this.NotesCollectionView.ItemsSource = this.viewModel.LoadNoteList();

        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            this.viewModel.ExamList.Clear();
            this.viewModel.ProjectList.Clear();
            this.viewModel.NoteList.Clear();

        }

        private void Save_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.UpdateCourse.Execute(null);
            DisplayAlert("Update Info", App.AcademicRepo.statusMessage.ToString(), "OK");
        }

        private void Delete_Button_Clicked(object sender, EventArgs e)
        {
            viewModel.DeleteCourse.Execute(null);
            Navigation.PopAsync();
        }

        async private void AddNote_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateNotePage(tempCourseId, tempTermId)));
        }

        private void AddStartNotification_Button_Clicked(object sender, EventArgs e)
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
            CrossLocalNotifications.Current.Show(viewModel.Course.CourseName, "This course starts on " + viewModel.Course.CourseStart.ToShortDateString() + "", NewId, StartDateNotification_Picker.Date);
            App.AcademicRepo.AddNewIdFromCourse(viewModel.Course.CourseId, viewModel.Course.TermId);
        }

        private void AddEndNotification_Button_Clicked(object sender, EventArgs e)
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
            CrossLocalNotifications.Current.Show(viewModel.Course.CourseName, "This course ends on " + viewModel.Course.CourseEnd.ToShortDateString() + "", NewId, EndDateNotification_Picker.Date);
            App.AcademicRepo.AddNewIdFromCourse(viewModel.Course.CourseId, viewModel.Course.TermId);
        }

        async private void AddExam_Btn_Clicked(object sender, EventArgs e)
        {
            if (this.viewModel.ExamList.Count < 3)
            {
                await Navigation.PushModalAsync(new NavigationPage(new CreateExamPage(tempCourseId, tempTermId)));
            }
            else
            {
                await DisplayAlert("Page Error", "This course can have only three Exams", "Ok");
            }
        }

        async private void AddProj_Btn_Clicked(object sender, EventArgs e)
        {
            if (this.viewModel.ProjectList.Count < 3)
            {
                await Navigation.PushModalAsync(new NavigationPage(new CreateProjectPage(tempCourseId, tempTermId)));
            }
            else
            {
                await DisplayAlert("Page Error", "This course can have only three Projects", "Ok");
            }
        }

        async private void Exam_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var obj = (Exam)layout.BindingContext;

            await Navigation.PushAsync(new ExamPage(new ExamViewModel(obj)));
        }

        async private void Project_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var per = (Project)layout.BindingContext;
            await Navigation.PushAsync(new ProjectPage(new ProjectViewModel(per)));
        }

        async private void Note_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var note = (Note)layout.BindingContext;
            await Navigation.PushAsync(new OptionalNotePage(new OptionalNotesViewModel(note)));
        }
    }
}