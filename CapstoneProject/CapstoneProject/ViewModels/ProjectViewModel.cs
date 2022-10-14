using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CapstoneProject.ViewModels
{
    public class ProjectViewModel
    {
        public Project Proj { get; set; }
        public Command UpdatePer { get; set; }
        public Command DeletePer { get; set; }

        public List<string> ProjectTypeList { get; set; }
        public List<string> ParticipantNumList { get; set; }


        public ProjectViewModel(Project proj = null)
        {

            Proj = proj ?? new Project();

            UpdatePer = new Command(ExecuteUpdate);
            DeletePer = new Command(ExecuteDelete);

            ProjectTypeList = App.AcademicRepo.GetProjectTypeList();
            ParticipantNumList = App.AcademicRepo.GetParticipantNumList();

        }


        public void ExecuteUpdate()
        {
            Project proj = Proj;
            App.AcademicRepo.UpdateProject(proj);
        }

        public void ExecuteDelete()
        {
            Project proj = Proj;
            int ProjId = proj.ProjectId;
            App.AcademicRepo.RemoveProject(ProjId);
            //App.page.Navigation.PopAsync();

        }
    }
}
