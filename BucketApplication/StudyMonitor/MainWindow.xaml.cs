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
        public ObservableCollection<StudyCaseControl> StudyCaseControllers { get; set; } = new ObservableCollection<StudyCaseControl>(); 
        private StudyTypes _studyTypes = new StudyTypes();

        public ObservableCollection<string> StudyTypes
        {
            get { return _studyTypes.StudyTypesCollection; }
            set { _studyTypes.StudyTypesCollection = value; }
        }

        public MainWindow()
        {

            StudyTypes = DatabaseAdapter.GetStudyTypes();
            var studyCases = DatabaseAdapter.GetStudyCases();

            foreach (var studyCase in studyCases)
            {
                StudyCaseControllers.Add(new StudyCaseControl(studyCase));
            }

            InitializeComponent();
        }

        /// <summary>
        /// Starts a study case
        /// </summary>
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartCase();
        }

        /// <summary>
        /// Stops and saves a study case
        /// </summary>
        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveStudyCase == null)
                return;

            StopCase();
            SaveCase();

            // Clean UI
            LabelStudyCaseStatus.Content = string.Empty;

            // Reset Active Study Case
            ActiveStudyCase = null;

        }

        /// <summary>
        /// Starts a study case
        /// Enables the stop button
        /// Disables the start button
        /// </summary>
        private void StartCase()
        {
            ActiveStudyCase = new StudyCase(comboBoxCaseType.Text);
            ActiveStudyCase.StartTimer();
            LabelStudyCaseStatus.Content = $"Running {ActiveStudyCase.StudyCaseType} ...\n";
            startButton.IsEnabled = false;
            stopButton.IsEnabled = true;
        }

        /// <summary>
        /// End a study case
        /// Enables the start button
        /// Disables the stop button
        /// </summary>
        private void StopCase()
        {
            ActiveStudyCase.StopTimer();
            ActiveStudyCase.Description = textBoxDescription.Text;
            startButton.IsEnabled = true;
            stopButton.IsEnabled = false;
        }

        /// <summary>
        /// Saves the study case to the database,
        /// Adds the study case to the window
        /// </summary>
        private void SaveCase()
        {
            DatabaseAdapter.SaveToDatabase(ActiveStudyCase);
            StudyCaseControllers.Add(new StudyCaseControl(ActiveStudyCase));
        }
    }
}
