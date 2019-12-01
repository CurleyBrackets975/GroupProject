using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAttempt
{
    class Question
    {
       
        public string Body { get; set; }
        public string Answer { get; set; }
        public string[] WrongAnswers { get; set; }
        public string Subject { get; set; }
        public int Chapter { get; set; }
        

        public Question()
        {
            Body = string.Empty;
            Answer = string.Empty;
            Subject = "Unlisted";
        }

        public Question (string body, string answer, string subject, string[] wrongAnswers, int chapter)
        {
            Body = body;
            Answer = answer;
            Subject = subject;
            WrongAnswers = wrongAnswers;
            Chapter = chapter;
        }

        //for incase they want us to make up random wrong answers
        public Question (string body, string answer, string subject, int chapter)
        {
            Body = body;
            Answer = answer;
            Subject = subject;
            Chapter = chapter;
        }

        public override string ToString ()
        {
            string stringForm = $"{Subject}, Chapter: {Chapter}, {Body}, {Answer}";


            
            foreach (string i in WrongAnswers)
            {
                stringForm += $", {i}";
            }

            return stringForm;
        }

        public string WrongAnswerString()
        {
            string temp = string.Empty;

            for (int i = 0; i < WrongAnswers.Length; i++)
            {
                temp += $"\"{WrongAnswers[i]}";

                if (i != WrongAnswers.Length - 1)
                {
                    temp += "\",";
                }
                else
                {
                    temp += "\"";
                }
            }
            return temp;
        }

    }
}
