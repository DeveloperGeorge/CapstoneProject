using CapstoneProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CapstoneProject.Database
{
    public class AcademicTrackerRepository
    {
        private readonly SQLiteConnection conn;


        private bool TermAdded;
        //private bool TermUpdated;
        //private bool CourseUpdated;
        private bool CourseAdded;
        //private bool IsPhoneNum;
        private bool NoteAdded;
        private bool NoteUpdated;
        private bool ExAdded;
        //private bool ExUpdated;
        private bool ProjAdded;
        //private bool ProjUpdated;
        private string StatusMessage;

        public readonly List<string> CourseStatus;
        public readonly List<string> ProjectTypes;
        public readonly List<string> QuestionTypes;
        public readonly List<string> ParticipantNum;
        public List<string> Assessments;

        public int currentUser { get; set; }
        public string statusMessage { get { return StatusMessage; } }
        public bool termAdded { get { return TermAdded; } }
        //public bool termUpdated { get { return TermUpdated; } }
        //public bool courseUpdated { get { return CourseUpdated; } }
        public bool courseAdded { get { return CourseAdded; } }
        //public bool isPhoneNum { get { return IsPhoneNum; } }
        public bool noteAdded { get { return NoteAdded; } }
        public bool noteUpdated { get { return NoteUpdated; } }
        public bool exAdded { get { return ExAdded; } }
        //public bool exUpdated { get { return ExUpdated; } }
        public bool projAdded { get { return ProjAdded; } }
        //public bool projUpdated { get { return ProjUpdated; } }
        public bool isPhoneNum;

        public AcademicTrackerRepository()
        {

        }

        public AcademicTrackerRepository(string dbPath)
        {


            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<User>();
            conn.CreateTable<Term>();
            conn.CreateTable<Course>();
            conn.CreateTable<Exam>();
            conn.CreateTable<Project>();
            conn.CreateTable<Note>();
            conn.CreateTable<NotificationId>();



            if (GetUserCount() == 0)
            {
                conn.Insert(new User { UserName = "test", Password = "test" });
                conn.Insert(new User { UserName = "test2", Password = "test2" }); // User 2 testing
                conn.Insert(new Term { TermName = "Term 1", TermStart = DateTime.Today, TermEnd = DateTime.Today.AddDays(1), UserId = 1 });
                conn.Insert(new Term { TermName = "User 2's term", TermStart = DateTime.Today, TermEnd = DateTime.Today.AddDays(1), UserId = 2 }); // User 2 term testing
                conn.Insert(new Course { TermId = 1, CourseName = "Course 1", CourseStart = DateTime.Today, CourseEnd = DateTime.Today.AddDays(1), Status = "Completed", CourseInstructor = "George Kort", PhoneNum = "324-0215", Email = "gkort@wgu.edu", UserId = 1 });
                conn.Insert(new Course { TermId = 2, CourseName = "User 2 Course", CourseStart = DateTime.Today, CourseEnd = DateTime.Today.AddDays(1), Status = "In Progress", CourseInstructor = "George Kort", PhoneNum = "324-0215", Email = "gkort@wgu.edu", UserId = 2 }); // User 2 testing course
                conn.Insert(new Note { CourseId = 1, TermId = 1, NoteName = "New Temp Note", NoteDes = "This is filler.This is filler.This is filler.This is filler.This is filler.This is filler.This is filler.This is filler." });
                conn.Insert(new Exam { CourseId = 1, TermId = 1, AssessmentName = "Exam Assessment", Start = DateTime.Now, End = DateTime.Now.AddDays(1), Des = "This is filler.This is filler.This is filler.This is filler.This is filler.This is filler.", QuestionTypes = "Multiple Choice", NumQuestions = "60", UserId = 1 });
                conn.Insert(new Exam { CourseId = 2, TermId = 2, AssessmentName = " User 2 Exam Assessment", Start = DateTime.Now, End = DateTime.Now.AddDays(1), Des = "This is filler.This is filler.This is filler.This is filler.This is filler.This is filler.", QuestionTypes = "Multiple Choice", NumQuestions = "60", UserId = 2 }); // User 2 testing exam
                conn.Insert(new Project { CourseId = 1, TermId = 1, AssessmentName = "Project Assessment", Start = DateTime.Now, End = DateTime.Now.AddDays(1), Des = "This is filler.This is filler.This is filler.This is filler.This is filler.This is filler.", ProjectType = "Turn In", ParticipantNum = "One", UserId = 1 });
            }



            CourseStatus = new List<string>
            {
                "In Progress",
                "Completed",
                "Plan to Take",
                "Dropped"
            };


            ProjectTypes = new List<string>
            {
                "Presentation",
                "Turn In"
            };


            ParticipantNum = new List<string>
            {
                "One",
                "Two",
                "Three",
                "Four",
                "Five",
                "Six"
            };


            QuestionTypes = new List<string>
            {
                "Unknown",
                "Practical",
                "Written",
                "Multiple Choice",
                "Mixed"
            };


        }

        public int GetUserCount()
        {
            int listcount;
            List<User> tempUserList = new List<User>();
            tempUserList = conn.Table<User>().ToList();

            listcount = tempUserList.Count;

            return listcount;


        }


        public string GetAssessmentCount()
        {
            List<Assessment> assessments = new List<Assessment>();
            List<Course> course = new List<Course>();
            int examCount = 0;
            int projCount = 0;
            string CurrentCount;
            string status;
            assessments = GetAllAssessments();
            foreach (Assessment a in assessments)
            {
                status = "";
                course = conn.Table<Course>().Where(c => c.CourseId == a.CourseId).ToList();
                status = course[0].Status;

                if (a.GetAssessmentType() == "Exam" && (status == "In Progress" || status == "Plan to Take"))
                {
                    examCount++;
                }
                else if (a.GetAssessmentType() == "Project" && (status == "In Progress" || status == "Plan to Take"))
                {
                    projCount++;
                }
                else
                {

                }

            }

            course.Clear();

            CurrentCount = string.Format("Exams {0} - {1} Projects", examCount, projCount);

            return CurrentCount;


        }


        public List<Assessment> GetAllAssessments()
        {
            List<Exam> TempExam;
            List<Project> TempProject;
            List<Assessment> TempAssessment = new List<Assessment>();
            try
            {
                TempExam = conn.Table<Exam>().Where(e=> e.UserId == currentUser).ToList();
                TempProject = conn.Table<Project>().Where(p => p.UserId == currentUser).ToList();

                foreach (Exam e in TempExam)
                {
                    TempAssessment.Add(e);
                }
                foreach (Project p in TempProject)
                {
                    TempAssessment.Add(p);
                }

                return TempAssessment;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Assessment>();
        }

        public List<Report> GetReports()
        {
            List<Term> tempTerms;
            List<Course> tempCourses;
            List<Report> tempReport = new List<Report>();
            
            try
            {
                tempTerms = conn.Table<Term>().Where(t => t.UserId == currentUser).ToList();
                tempCourses = conn.Table<Course>().ToList();
                for (int i = 0; i < tempTerms.Count; i++)
                {
                    Report report = new Report();
                    List<string> ntemplist = new List<string>();
                    
                    for (int j = 0; j < tempCourses.Count; j++)
                    {
                        

                        if(tempCourses[j].TermId == tempTerms[i].TermId)
                        {
                            ntemplist.Add(tempCourses[j].CourseName.ToString());
                            
                            
                        }
                   
                        
                    }

                    report.TermName = tempTerms[i].TermName;
                    report.CourseList = ntemplist;
                    tempReport.Add(report);
                    
                }
                return tempReport;

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Report>();
        }


        public List<string> GetCourseStatusList()
        {
            return CourseStatus;
        }

        public List<string> GetQuestionTypeList()
        {
            return QuestionTypes;
        }

        public List<string> GetProjectTypeList()
        {
            return ProjectTypes;
        }

        public List<string> GetParticipantNumList()
        {
            return ParticipantNum;
        }

        public void AddNewTerm(string termName, DateTime termStart, DateTime termEnd)
        {


            try
            {
                if (string.IsNullOrEmpty(termName))
                    throw new Exception("Valid name required");
                else if (termEnd <= termStart)
                    throw new Exception("End date must take place after Start date");
                conn.Insert(new Term { TermName = termName, TermStart = termStart, TermEnd = termEnd, UserId = currentUser });

                StatusMessage = string.Format("{0} Term added", termName);
                TermAdded = true;

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add new term {0}. Error: {1}", termName, ex.Message);
                TermAdded = false;
            }


        }


        public void UpdateTerm(Term term)
        {
            try
            {
                string tName = term.TermName;


                if (string.IsNullOrEmpty(term.TermName))
                    throw new Exception("Valid name required");
                else if (term.TermEnd <= term.TermStart)
                    throw new Exception("End date must take place after start date");
                conn.Update(term);
                StatusMessage = string.Format("{0} Term updated", tName);
                //TermUpdated = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update term {0} . Error: {1}", term.TermName, ex.Message);
                //TermUpdated = false;
            }
        }

        public void RemoveTerm(int termId)
        {
            conn.Table<NotificationId>().Delete(not => not.TermId == termId);
            conn.Table<Project>().Delete(proj => proj.TermId == termId);
            conn.Table<Exam>().Delete(exam => exam.TermId == termId);
            conn.Table<Note>().Delete(note => note.TermId == termId);
            conn.Table<Course>().Delete(Course => Course.TermId == termId);
            conn.Delete<Term>(termId);
        }

        public void UpdateCourse(Course course)
        {
            try
            {
                string cName = course.CourseName;


                if (string.IsNullOrEmpty(course.CourseName))
                    throw new Exception("Valid course name required");
                else if (course.CourseEnd <= course.CourseStart)
                    throw new Exception("End date must take place after start date");
                else if (string.IsNullOrEmpty(course.CourseInstructor))
                    throw new Exception("Instructor name is empty");
                else if (!CheckPhone(course.PhoneNum))
                    throw new Exception("Instructor phone number not valid. ex ###-####");
                else if (!CheckEmail(course.Email))
                    throw new Exception("Not a valid email");
                conn.Update(course);
                StatusMessage = string.Format("{0} updated", cName);
                //CourseUpdated = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update course {0} . Error: {1}", course.CourseName, ex.Message);
                //CourseUpdated = false;
            }
        }

        public void RemoveCourse(int courseId)
        {
            conn.Table<NotificationId>().Delete(not => not.CourseId == courseId);
            conn.Table<Project>().Delete(proj => proj.CourseId == courseId);
            conn.Table<Exam>().Delete(exam => exam.CourseId == courseId);
            conn.Table<Note>().Delete(note => note.CourseId == courseId);
            conn.Delete<Course>(courseId);
        }





        public List<Term> GetAllTerms()
        {
            try
            {
                //return conn.Table<Term>().ToList();
                return conn.Table<Term>().Where(t => t.UserId == currentUser).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Term>();
        }

        public List<Term> GetSearchTermsList(string search)
        {
            try
            {
                List<Term> SearchTermList = new List<Term>();
                List<Term> tempSearchList = new List<Term>();
                tempSearchList  = conn.Table<Term>().Where(t => t.UserId == currentUser).ToList();
                foreach(Term t in tempSearchList)
                {
                    if(t.UserId == currentUser && t.TermName.ToUpper().Contains(search.ToUpper()))
                    {
                        SearchTermList.Add(t);
                    }
                }

                return SearchTermList;
                //return conn.Table<Term>().Where(term => term.TermName.ToUpper().Contains(search.ToUpper())).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<Term>();
        }

        public List<Course> GetAllCourses(int id)
        {
            try
            {

                return conn.Table<Course>().Where(course => course.TermId == id).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Course>();
        }




        public void AddNewCourse(int termId, string courseName, DateTime courseStart, DateTime courseEnd, string courseStatus, string courseInstructor, string instructorNum, string instructorEmail)
        {
            try
            {
                if (string.IsNullOrEmpty(courseName))
                    throw new Exception("Course name required");
                else if (courseEnd <= courseStart)
                    throw new Exception("End date must take place after start date");
                else if (string.IsNullOrEmpty(courseStatus))
                    throw new Exception("Must select course status");
                else if (string.IsNullOrEmpty(courseInstructor))
                    throw new Exception("Instructor name is empty");
                else if (!CheckPhone(instructorNum))
                    throw new Exception("Instructor phone number not valid. ex ###-####");
                else if (!CheckEmail(instructorEmail))
                    throw new Exception("Not a valid email");
                conn.Insert(new Course { TermId = termId, CourseName = courseName, CourseStart = courseStart, CourseEnd = courseEnd, Status = courseStatus, CourseInstructor = courseInstructor, PhoneNum = instructorNum, Email = instructorEmail, UserId = currentUser });

                StatusMessage = string.Format("{0} term added", courseName);
                CourseAdded = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add new course {0}. Error: {1}", courseName, ex.Message);
                CourseAdded = false;
            }
        }

        public bool CheckPhone(string s)
        {
            Regex phoneRegEx = new Regex(@"^\d{3}-\d{4}$");
            if (phoneRegEx.IsMatch(s))
            {
                isPhoneNum = true;
                return true;
            }
            else
            {
                isPhoneNum = false;
                return false;
            }
        }

        public bool CheckEmail(string s)
        {
            Regex emailRegEx = new Regex(@"^([a-zA-Z0-9\.-]+)@([a-zA-Z0-9]+)\.([a-zA-Z0-9]+)$");
            if (emailRegEx.IsMatch(s))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Project> GetAllProjects(int id)
        {
            try
            {

                return conn.Table<Project>().Where(proj => proj.CourseId == id).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Project>();
        }


        public List<Exam> GetAllExams(int id)
        {
            try
            {

                return conn.Table<Exam>().Where(exam => exam.CourseId == id).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Exam>();
        }


        public List<Note> GetAllNotes(int id)
        {
            try
            {

                return conn.Table<Note>().Where(note => note.CourseId == id).ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<Note>();
        }

        public void AddNewNote(int termId, int courseId, string noteName, string noteDes)
        {
            try
            {
                if (string.IsNullOrEmpty(noteName))
                    throw new Exception("Note name required");

                conn.Insert(new Note { TermId = termId, CourseId = courseId, NoteName = noteName, NoteDes = noteDes });

                StatusMessage = string.Format("{0} note added", noteName);
                NoteAdded = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add new note: {0}. Error: {1}", noteName, ex.Message);
                NoteAdded = false;
            }
        }


        public void UpdateNote(Note note)
        {
            try
            {
                string nName = note.NoteName;


                if (string.IsNullOrEmpty(note.NoteName))
                    throw new Exception("Valid name required");
                conn.Update(note);
                StatusMessage = string.Format("{0} note updated", nName);
                NoteUpdated = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update note {0} . Error: {1}", note.NoteName, ex.Message);
                NoteUpdated = false;
            }
        }

        public void RemoveNote(int noteId)
        {
            conn.Delete<Note>(noteId);
        }



        public void AddNewExam(int termId, int courseId, string exName, DateTime exStart, DateTime exEnd, string exDes, string qType, string qCount)
        {
            try
            {
                if (string.IsNullOrEmpty(exName))
                    throw new Exception("Name required");
                else if (exEnd <= exStart)
                    throw new Exception("End date must take place after start date");
                else if (string.IsNullOrEmpty(qType))
                    throw new Exception("Must select question type");
                else if (string.IsNullOrEmpty(qCount))
                    throw new Exception("Question Count must have a number");
                else if (qCount.Contains("-"))
                    throw new Exception("Question Count must be zero or higher");
                conn.Insert(new Exam { TermId = termId, CourseId = courseId, AssessmentName = exName, Start = exStart, End = exEnd, Des = exDes, QuestionTypes = qType, NumQuestions = qCount, UserId = currentUser });

                StatusMessage = string.Format("{0} exam added", exName);
                ExAdded = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add new exam {0}. Error: {1}", exName, ex.Message);
                ExAdded = false;
            }
        }




        public void UpdateExam(Exam exam)
        {
            try
            {
                string exName = exam.AssessmentName;


                if (string.IsNullOrEmpty(exam.AssessmentName))
                    throw new Exception("Valid name required");
                else if (exam.End <= exam.Start)
                    throw new Exception("End date must take place after start date");
                else if (string.IsNullOrEmpty(exam.NumQuestions))
                    throw new Exception("Question Count must have a number");
                else if (exam.NumQuestions.Contains("-"))
                    throw new Exception("Question Count must be zero or higher");
                conn.Update(exam);
                StatusMessage = string.Format("{0} updated", exName);
                //ExUpdated = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update exam {0} . Error: {1}", exam.AssessmentName, ex.Message);
                //ExUpdated = false;
            }
        }



        public void RemoveExam(int exId)
        {
            conn.Table<NotificationId>().Delete(not => not.ExamId == exId);
            conn.Delete<Exam>(exId);
        }


        public void AddNewProject(int termId, int courseId, string projName, DateTime projStart, DateTime projEnd, string projDes, string projType, string parCount)
        {
            try
            {
                if (string.IsNullOrEmpty(projName))
                    throw new Exception("Name required");
                else if (projEnd <= projStart)
                    throw new Exception("End date must take place after start date");
                else if (string.IsNullOrEmpty(projType))
                    throw new Exception("Please select a project type");
                else if (string.IsNullOrEmpty(parCount))
                    throw new Exception("Please select the number of participants");
                conn.Insert(new Project { TermId = termId, CourseId = courseId, AssessmentName = projName, Start = projStart, End = projEnd, Des = projDes, ProjectType = projType, ParticipantNum = parCount, UserId = currentUser });

                StatusMessage = string.Format("{0} project added", projName);
                ProjAdded = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add new project {0}. Error: {1}", projName, ex.Message);
                ProjAdded = false;
            }
        }



        public void UpdateProject(Project proj)
        {
            try
            {
                string projName = proj.AssessmentName;


                if (string.IsNullOrEmpty(proj.AssessmentName))
                    throw new Exception("Valid name required");
                else if (proj.End <= proj.Start)
                    throw new Exception("End date must take place after start date");
                conn.Update(proj);
                StatusMessage = string.Format("{0} updated", projName);
                //ProjUpdated = true;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to update project {0} . Error: {1}", proj.AssessmentName, ex.Message);
                //ProjUpdated = false;
            }
        }



        public void RemoveProject(int projId)
        {
            conn.Table<NotificationId>().Delete(not => not.ProjectId == projId);
            conn.Delete<Project>(projId);
        }



        public List<NotificationId> GetNotificationId()
        {
            try
            {
                return conn.Table<NotificationId>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<NotificationId>();
        }

        public void AddNewIdFromCourse(int courseId, int termId)
        {
            conn.Insert(new NotificationId { CourseId = courseId, TermId = termId });
        }

        public void AddNewIdFromProject(int courseId, int termId, int projId)
        {
            conn.Insert(new NotificationId { CourseId = courseId, TermId = termId, ProjectId = projId });
        }

        public void AddNewIdFromExam(int courseId, int termId, int examId)
        {
            conn.Insert(new NotificationId { CourseId = courseId, TermId = termId, ExamId = examId });
        }


        public bool CheckLogin(string username, string password)
        {
            List<User> users = new List<User>();
            users = conn.Table<User>().Where(c => c.UserName == username).ToList();
            bool loginbool = false;

            foreach (User c in users)
            {

                if (c.UserName == username && c.Password == password)
                {
                    currentUser = c.UserId;
                    loginbool = true;
                    break;

                }
                else
                {

                }

            }

            return loginbool;

        }



    



    }
}
