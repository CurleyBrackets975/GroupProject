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

namespace FirstAttempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //This will read from a JSON file by the end
        static readonly string[] SUBJECTS = { "History", "Math", "English", "Science" };


        //This will also be a JSON file later
        static List<Question> QUESTIONS = new List<Question>();
        public MainWindow()
        {
            InitializeComponent();

            using (StreamReader r = new StreamReader("file.json"))
            {
                string json = r.ReadToEnd();
                QUESTIONS = JsonConvert.DeserializeObject<List<Question>>(json);
            }

            //add all the subjects to the form
            foreach (string i in SUBJECTS)
            {
                CBXSubjects.Items.Add(i);
            }
        }

        private void BTNAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            //generates question from inputs assuming perfect entry            
            Question temp = new Question(TBXQuestion.Text, TBXAnswer.Text, CBXSubjects.Text, TBXWrongAnswer.Text.Split(','), Convert.ToInt32(TBXChapter.Text));
            if (!QUESTIONS.Contains(temp))
            {
                QUESTIONS.Add(temp);
            }
            else
            {
                MessageBox.Show("The question submitted already exists");
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

        private void BTNGenerate_Click(object sender, RoutedEventArgs e)
        {
            Homework temp = new Homework(CBXSubjects.SelectedValue.ToString(), TBXName.Text, Convert.ToInt32(TBXNumber.Text));
        }

        private void LBXHomework_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
