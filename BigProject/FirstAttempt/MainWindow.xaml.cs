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
using System.Diagnostics;

namespace FirstAttempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //This updates the combo box with the name of the subjects added
        static readonly string[] SUBJECTS = { "History", "Math", "MIS", "B AD" };

        static List<Question> QUESTIONS = new List<Question>();

        public MainWindow()
        {
            InitializeComponent();
            //reading the json file in the debug folder
            using (StreamReader sr = new StreamReader("Questions.json"))
            {
                string json = sr.ReadToEnd();
                QUESTIONS = JsonConvert.DeserializeObject<List<Question>>(json);
            }
            //add all subjects to the subject combo box which we declared up above
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

            //Create JSON string adding question into it
            var json = JsonConvert.SerializeObject(QUESTIONS);
            //declaring path for the json file and telling json to write in it
            var path = "Questions.JSON";
            using (StreamWriter sr = File.CreateText(path))
            {
                sr.WriteLine(json);
                sr.Close();
            }

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
        //when the generate html button is clicked this section is opening that file and formating the HTML with the homework questions
        private void BTNGenerate_Click(object sender, RoutedEventArgs e)
        {
            //counting variable for formating
            int count = 0;
            //declared this variable for html so would not have to use the whole formating for each line
            string HTML = "<HTML>";
            //this is how the file is titled when the user enters in document name
            HTML = HTML + "<h1>" + (TBXName.Text) + "</h1>";
            //
            foreach (Question item in QUESTIONS)
            {

                //If no subject is selected, then export all subjects and if subject is not selected then show all subjects in html
                if (CBXSubjects.Text == item.Subject || CBXSubjects.Text == "")
                {
                    //Subject and chapter are on the same line
                    HTML = HTML + "<div style='font - size:20px;'>" + (item.Subject.ToString()) + ": Chapter " + (item.Chapter.ToString()) + "<br>" + "</div>";
                    //the count+1 is formating each question to be numbered
                    HTML = HTML + "<div>" + (count + 1).ToString() + ". " + (item.Body.ToString()) + "</div>";
                    HTML = HTML + "<ul>";
                    //this is for the show correct answer combo box - when yes is selected then the correct answers will be colored green
                    if (CBXYes.IsSelected)
                    {
                        HTML = HTML + "<li style='color: green; font - size:12px;'>" + (item.Answer.ToString());
                    }
                    //if the no or nothing is selected then the answer will not be colored green
                    else
                    {
                        HTML = HTML + "<li style= font - size:12px;'>" + (item.Answer.ToString());
                    }
                    //since wrong answer is a list we need a foreach loop to go through and do it for each wrong answer
                    foreach (string wronganswer in item.WrongAnswers)
                    {    //this is for showing of the wrong answers now. when the yes is selected then the wrong answers will be colored red
                        //and if it is not selected or no is selected it will just be black
                        if (CBXYes.IsSelected)
                        {
                            HTML = HTML + "<li  style='color: red; font - size:12px; '> " + (wronganswer.ToString()) + "</Li>";
                        }
                        else
                        {
                            HTML = HTML + "<li  style= font - size:12px; '> " + (wronganswer.ToString()) + "</Li>";
                        }
                    }
                    HTML = HTML + "</ul>";
                    count++;
                }
            }
            //closing html formating
            HTML = HTML + "</HTML>";
            // if no questions for a selected subject are found this is the error message
            if (count == 0)
            {
                MessageBox.Show("No questions for this subject are found!");
            }
            else
            {
                //declaring path for the HTML file
                var path = "Questions.HTML";
                //this is how the html file is being writen by the choices from the user
                using (StreamWriter sr = File.CreateText(path))
                {
                    sr.WriteLine(HTML);
                    sr.Close();

                    MessageBox.Show("HTML has been created! Will now open your browser.");
                    //this is the code for how the html opens on any computer as long as the Questions.HTML file is in the debug folder
                    try
                    {
                        using (Process myProcess = new Process())
                        {
                            string myDir = Directory.GetCurrentDirectory();
                            myProcess.StartInfo.UseShellExecute = false;
                            //this is opening the file in chrome browser. Could change if wanted.
                            Process.Start("Chrome.exe", myDir + "\\" + path);
                            myProcess.Start();
                        }
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2.Message);
                    }
                }
            }
        }


        private void LBXHomework_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}






