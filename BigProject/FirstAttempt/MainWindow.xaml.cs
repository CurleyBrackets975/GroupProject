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
        static readonly List<Question> QUESTIONS = new List<Question>();
        public MainWindow()
        {
            InitializeComponent();
            
            //add all the subjects to the form
            foreach (string i in SUBJECTS)
            {
                CBXSubjects.Items.Add(i);
            }
        }

        private void BTNAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            //generates question from inputs assuming perfect entry
            Question temp = new Question(TBXQuestion.Text, TBXAnswer.Text, CBXSubjects.Text, TBXWrongAnswer.Text.Split(','));
            QUESTIONS.Add(temp);

            //clear box to avoid simulated duplication and apply the legend
            LBXHomework.Items.Clear();
            LBXHomework.Items.Add("Subject, Question, Answer, Wrong Answers");

            //add the whole list to the lbx
            foreach (Question i in QUESTIONS)
            {
                LBXHomework.Items.Add(i.ToString());
            }
        }

        private void BTNGenerate_Click(object sender, RoutedEventArgs e)
        {
            //how we're goning to generate a homework of a specific length
            for (int i = 0; i < Convert.ToInt32(TBXNumber.Text); i++)
            {
                
            }
        }

        private void LBXHomework_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
