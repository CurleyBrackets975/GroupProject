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
        public string WrongAnswer { get; set; }
        public string Subject { get; set; }
        

        public Question()
        {
            Body = string.Empty;
            Answer = string.Empty;
            WrongAnswer = string.Empty;
            Subject = "Unlisted";
        }

        public Question (string body, string answer, string subject)
        {
            Body = body;
            Answer = answer;
            Subject = subject;
        }

    }
}
