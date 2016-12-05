using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace StudyMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public StudyCase ActiveStudyCase;
        public List<StudyCase> StudyCases = new List<StudyCase>(); 
        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBox();
            var studyCases = DatabaseAdapter.GetStudyCases();
            if (studyCases != null)
            {
                StudyCases = studyCases;
            }
            foreach (var studyCase in studyCases)
            {
                textBlockCases.Text += studyCase + "\n";
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if(ActiveStudyCase == null)
                newCaseButton_Click(sender, e);

            ActiveStudyCase.StartTimer();
            textBlockCases.Text += $"Starting new {ActiveStudyCase.StudyCaseType} \n";

        }

        private void newCaseButton_Click(object sender, RoutedEventArgs e)
        {
            ActiveStudyCase = new StudyCase(comboBoxCaseType.Text);
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveStudyCase == null)
                return;

            ActiveStudyCase.StopTimer();
            ActiveStudyCase.Description = textBoxDescription.Text;

            textBlockCases.Text += ActiveStudyCase + "\n";

            DatabaseAdapter.SaveToDatabase(ActiveStudyCase);
            StudyCases.Add(ActiveStudyCase);
            ActiveStudyCase = null;
        }

        private void InitializeComboBox()
        {
            var studyTypes = DatabaseAdapter.GetStudyTypes();

            foreach (var studyType in studyTypes)
            {
                comboBoxCaseType.Items.Add(studyType);
            }
        }
    }
}
