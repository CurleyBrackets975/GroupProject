using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace FirstAttempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //This will read from a JSON file by the end
        static readonly string[] SUBJECTS = { "History", "Math", "English", "Science" };



        static List<Question> QUESTIONS = new List<Question>();
        

        public MainWindow()
        {
            InitializeComponent();

            using (StreamReader sr = new StreamReader("Questions.json"))
            {
               // string[] line;

                //JObject o = JObject.Parse(sr.ReadToEnd());
                //string b = (string)o["Body"];
                //Console.WriteLine(b);
                //Console.ReadLine();
                string json = sr.ReadToEnd();
                QUESTIONS = JsonConvert.DeserializeObject<List<Question>>(json);
            }

            //do
            //{
            //    line = sr.ReadLine().Split(',');

            //    string[] wrongAnswers = line[3].Split(',');
            //    Question temp = new Question(line[0], line[1], line[2], wrongAnswers, Convert.ToInt32(line[3]));
            //} while (sr.ReadLine() != null);
       // }

            //add all the subjects to the form
            foreach (string i in SUBJECTS)
            {
                CBXSubjects.Items.Add(i);
            }
        }

        private void BTNAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            //generates question from inputs assuming perfect entry            
            Question temp = new Question(TBXQuestion.Text, TBXAnswer.Text, TBXSubject.Text, TBXWrongAnswer.Text.Split(','), Convert.ToInt32(TBXChapter.Text));
            QUESTIONS.Add(temp);

            //Create JSON string with questions in it
            var json = JsonConvert.SerializeObject(QUESTIONS);
            //StreamWriter wr = new StreamWriter("QuestionsJSON.txt");


            var path="Questions.JSON";
            using (StreamWriter sr = File.CreateText(path))
            {
                sr.WriteLine(json);
                sr.Close();

                //Console.WriteLine(File.ReadAllText(QUESTIONS));
                // MessageBox.Show("The question has been entered.");

            }




            ////temporarily removing duplicate checking - Sabrina 12/1
            //if (!QUESTIONS.Contains(temp))
            //{
            //    QUESTIONS.Add(temp);
            //    StreamWriter wr = new StreamWriter("QuestionsJSON.txt");
            //    wr.WriteLine($"{{\n\"Body\" : \"{temp.Body}\",\n\"Answer\" : \"{temp.Answer}\",\n\"Subject\" : \"{temp.Subject}\",\n\"Chapter\" : \"{temp.Chapter}\",\n\"WrongAnswer\" : [\"{temp.WrongAnswerString()}\"]\n}}");
            //    MessageBox.Show("The question has been entered.");
            //}
            //else
            //{
            //    MessageBox.Show("The question submitted already exists");
            //}

            /* Old way of outputting specific questions 
            //clear box to avoid simulated duplication and apply the legend
            LBXHomework.Items.Clear();
            LBXHomework.Items.Add("Subject, Question, Answer, Wrong Answers");

            //add the whole list to the lbx
            foreach (Question i in QUESTIONS)
            {
                LBXHomework.Items.Add(i.ToString());
                TBXQuestion.Clear();
                TBXAnswer.Clear();
                TBXWrongAnswer.Clear();
            }
            */


        }

        private void BTNGenerate_Click(object sender, RoutedEventArgs e)
        {
            string HTML ="<HTML>";
            foreach (Question item in QUESTIONS)
            {
                HTML=HTML+ "<li>" + (item.Subject.ToString())+"</li>";
                HTML = HTML + "<ul>";
                HTML = HTML + "<li>" + (item.Chapter.ToString()) + "</li>";
                HTML = HTML + "<li>" + (item.Body.ToString()) + "</li>";
                HTML = HTML + "<li>" + (item.Answer.ToString()) + "</li>";
                HTML = HTML + "<li>" + (item.WrongAnswers.ToString()) + "</li>";
                HTML = HTML + "</ul>";
            }
            HTML = HTML + "</HTML>";
            var path = "Questions.HTML";
            using (StreamWriter sr = File.CreateText(path))
            {
                sr.WriteLine(HTML);
                sr.Close();


                MessageBox.Show("HTML has been created.");

            }
            }

            private void LBXHomework_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
