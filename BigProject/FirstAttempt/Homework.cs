using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

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

        public Homework(string subject,string name, int numQuestions)
        {
            if (getQuestionsBySubject(subject).Count > numQuestions)
            {
                Subject = subject;
                Name = name;
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
            else
            {
                MessageBox.Show("There are not enough unique questions of that subject.");
            }
        }

        private List<Question> getQuestionsBySubject(string subject)
        {
            List<Question> sortedBySubject = new List<Question>();

            foreach (Question q in Questions)
            {
                if (q.Subject == this.Subject)
                {
                    sortedBySubject.Add(q);
                }
            }

            return sortedBySubject;
        }

        //this method will get a question with the homeworks subject
        private Question getQuestion ()
        {
            Random r = new Random();
            List<Question> temp = getQuestionsBySubject(this.Subject);
            int number = r.Next(0, temp.Count() + 1);
            if (!Questions.Contains(temp[number]))
            {
                return temp[number];
            }
            else
            {
                return this.getQuestion();
            }



        }

        public void printHomework()
        {

            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{Semester}{Year}{Name}";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(Name);
                    sw.WriteLine($"{Year} - {Semester}");
                    sw.WriteLine(Subject);
                    sw.WriteLine();
                    foreach (Question q in Questions)
                    {
                        sw.WriteLine(q.Body);
                        sw.WriteLine(this.genSelections(q));
                    }
                }
            }
        }

        private string genSelections(Question q)
        {
            string selections = null;
            Random r = new Random();
            int correctPlace = r.Next(1, 5);

            //check to see if there are predetermined wrong answers
            if (q.WrongAnswers != null)
            {
                //if yes we'll pull from those
                switch (correctPlace)
                {
                    case 1:
                        return $"A {q.Answer}\tB {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\nC {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\tD {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}";
                    case 2:
                        return $"A {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\tB {q.Answer}\nC {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\tD {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}";
                    case 3:
                        return $"A {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\tB {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\nC {q.Answer}\tD {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}";
                    case 4:
                        return $"A {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\tB {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\nC {q.WrongAnswers[r.Next(0, q.WrongAnswers.Length)]}\tD {q.Answer}";

                }
            }
            //if not we'll pull from other questions on this assignment
            else
            {
                switch (correctPlace)
                {
                    case 1:
                        return $"A {q.Answer}\tB {Questions[r.Next(0,Questions.Count)].Answer}\nC {Questions[r.Next(0,Questions.Count)].Answer}\tD {Questions[r.Next(0,Questions.Count)].Answer}";
                    case 2:
                        return $"A {Questions[r.Next(0,Questions.Count)].Answer}\tB {q.Answer}\nC {Questions[r.Next(0,Questions.Count)].Answer}\tD {Questions[r.Next(0,Questions.Count)].Answer}";
                    case 3:
                        return $"A {Questions[r.Next(0,Questions.Count)].Answer}\tB {Questions[r.Next(0,Questions.Count)].Answer}\nC {q.Answer}\tD {Questions[r.Next(0,Questions.Count)].Answer}";
                    case 4:
                        return $"A {Questions[r.Next(0,Questions.Count)].Answer}\tB {Questions[r.Next(0,Questions.Count)].Answer}\nC {Questions[r.Next(0,Questions.Count)].Answer}\tD {q.Answer}";

                }
            }


            return selections;
        }
    }
}
