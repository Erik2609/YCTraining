using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private StudyTypes _studyTypes = new StudyTypes();

        public ObservableCollection<string> StudyTypes
        {
            get { return _studyTypes.StudyTypesCollection; }
            set { _studyTypes.StudyTypesCollection = value; }
        }

        public MainWindow()
        {

            this.StudyTypes = DatabaseAdapter.GetStudyTypes();
            var studyCases = DatabaseAdapter.GetStudyCases();

            InitializeComponent();

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
            StartCase();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveStudyCase == null)
                return;

            StopCase();
            SaveCase();

            // Clean UI
            textBlockCases.Text += ActiveStudyCase + "\n";
            LabelStudyCaseStatus.Content = string.Empty;


            ActiveStudyCase = null;



        }

        private void StartCase()
        {
            ActiveStudyCase = new StudyCase(comboBoxCaseType.Text);
            ActiveStudyCase.StartTimer();
            LabelStudyCaseStatus.Content = $"Running {ActiveStudyCase.StudyCaseType} ...\n";
            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
        }

        private void StopCase()
        {
            ActiveStudyCase.StopTimer();
            ActiveStudyCase.Description = textBoxDescription.Text;
            startButton.IsEnabled = true;
            stopButton.IsEnabled = false;
        }

        private void SaveCase()
        {
            DatabaseAdapter.SaveToDatabase(ActiveStudyCase);
            StudyCases.Add(ActiveStudyCase);
        }
    }
}
