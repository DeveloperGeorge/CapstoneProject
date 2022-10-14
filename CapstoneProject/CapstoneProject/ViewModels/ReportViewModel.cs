using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProject.ViewModels
{
    public class ReportViewModel
    {
        public List<Report> ReportList { get; set; }
        public string courselist {get; set; }
        public DateTime Today { get; set; }
        public ReportViewModel()
        {

            ReportList = App.AcademicRepo.GetReports();
            Today = DateTime.Now;
            
        }



    }
}
