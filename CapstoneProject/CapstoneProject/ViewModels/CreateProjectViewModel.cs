using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.ViewModels
{
    public class CreateProjectViewModel
    {
        public List<string> ProjectTypeList { get; set; }
        public List<string> ParticipantNumList { get; set; }


        public CreateProjectViewModel()
        {
            ProjectTypeList = App.AcademicRepo.GetProjectTypeList();
            ParticipantNumList = App.AcademicRepo.GetParticipantNumList();
        }

    }
}
