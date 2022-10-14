using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.ViewModels
{
    public class CreateTermPageViewModel
    {
        public string TempTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public CreateTermPageViewModel()
        {
            TempTitle = "Term Title";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);


        }
    }
}
