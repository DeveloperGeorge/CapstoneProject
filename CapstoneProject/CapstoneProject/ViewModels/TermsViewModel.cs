using CapstoneProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CapstoneProject.ViewModels
{
    public class TermsViewModel :  ObservableCollection<Term>
    {
        public ObservableCollection<Term> TermsList { get; set; }
        public List<Term> termsList { get; set; }



        public TermsViewModel()
        {


            termsList = App.AcademicRepo.GetAllTerms();
            TermsList = new ObservableCollection<Term>(termsList);




        }



        public ObservableCollection<Term> LoadTermList()
        {
            termsList = App.AcademicRepo.GetAllTerms();
            return TermsList = new ObservableCollection<Term>(termsList);
        }

        public ObservableCollection<Term> LoadSearchTermList(string search)
        {
            termsList = App.AcademicRepo.GetSearchTermsList(search);
            return TermsList = new ObservableCollection<Term>(termsList);
        }

        public string GetAssessmentCount()
        {
            string currentCount;
            currentCount = App.AcademicRepo.GetAssessmentCount();

            return currentCount;

        }

    }
}
