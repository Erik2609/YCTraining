using System;

namespace StudyMonitor
{
    public class StudyCase
    {
        private readonly string _studyCaseType;
        private string _description;
        private DateTime _dateOfStudy;

        #region Properties

        public string StudyCaseType
        {
            get { return _studyCaseType; }
        }

        protected DateTime StartTime { get; set; }
        public TimeSpan TimeSpent { get; set; }

        public DateTime DateOfStudy
        {
            get { return _dateOfStudy; }
            set { _dateOfStudy = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region Constructors

        public StudyCase(string caseType)
        {
            _studyCaseType = caseType;
        }

        public StudyCase(string caseType, string description, TimeSpan timeSpan, DateTime dateOfStudy) : this(caseType)
        {
            Description = description;
            TimeSpent = timeSpan;
            DateOfStudy = dateOfStudy;
        }

        #endregion

        #region Methods
        public void StartTimer()
        {
            StartTime = DateTime.Now;
        }

        public void StopTimer()
        {
            TimeSpent = DateTime.Now - StartTime;
            DateOfStudy = new DateTime(StartTime.Year, StartTime.Month, StartTime.Day);
        }

        public string GetTimeSpent()
        {
            return $"Days: {TimeSpent.Days}, Hours: {TimeSpent.Hours}, Minutes: {TimeSpent.Minutes}, Seconds {TimeSpent.Seconds}";
        }

        public override string ToString()
        {
            return $"{DateOfStudy.ToShortDateString()}, {StudyCaseType}: for {TimeSpent.Hours} hours, {TimeSpent.Minutes} minutes and {TimeSpent.Seconds} seconds.";
        }

        #endregion

        #region Events

        #endregion
    }
}
