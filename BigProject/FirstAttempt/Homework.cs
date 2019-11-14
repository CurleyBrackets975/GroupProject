using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAttempt
{

    class Homework
    {
        private string[] FALL = { "august", "september", "october", "november", "december" };
        private string[] SPRING = { "january", "february", "march", "april", "may" };
        private string[] SUMMER = { "june", "july" };

        public string Subject;
        public string Name;
        public int Year;
        public string Semester;

        public List<Question> Questions = new List<Question>();

        public Homework()
        {
            Subject = string.Empty;
            Name = string.Empty;
            Year = DateTime.Today.Year;

            if(FALL.Contains(DateTime.Today.Month.ToString().ToLower()))
            {
                Semester = "Fall";
            }
            else if(SPRING.Contains(DateTime.Today.Month.ToString().ToLower()))
            {
                Semester = "Spring";
            }
            else
            {
                Semester = "Summer";
            }

        }

        public Homework(string subject, string name, int numQuestions)
        {
            Subject = string.Empty;
            Name = string.Empty;
            Year = DateTime.Today.Year;

            if (FALL.Contains(DateTime.Today.Month.ToString().ToLower()))
            {
                Semester = "Fall";
            }
            else if (SPRING.Contains(DateTime.Today.Month.ToString().ToLower()))
            {
                Semester = "Spring";
            }
            else
            {
                Semester = "Summer";
            }

            for (int i = 0; i < numQuestions; i++)
            {
                Questions.Add(this.getQuestion());
            }
        }

        //this method will get a question with the homeworks subject
        private Question getQuestion ()
        {
            return null;
        }
    }
}
