using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudyMonitor
{
    public class StudyCase
    {
        private readonly string _studyCaseType = "StudyCase";
        private string _description;

        #region Properties

        public string StudyCaseType
        {
            get { return _studyCaseType; }
        }

        protected DateTime StartTime { get; set; }
        public TimeSpan TimeSpent { get; set; }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion

        #region Constructors

        public StudyCase(string caseType)
        {
            this._studyCaseType = caseType;
        }

        public StudyCase(string caseType, string description, TimeSpan timeSpan) : this(caseType)
        {
            this.Description = description;
            this.TimeSpent = timeSpan;
        }

        #endregion

        #region Methods
        public void StartTimer()
        {
            this.StartTime = DateTime.Now;
        }

        public void StopTimer()
        {
            this.TimeSpent = DateTime.Now - this.StartTime;
        }

        public string GetTimeSpent()
        {
            return $"Days: {TimeSpent.Days}, Hours: {TimeSpent.Hours}, Minutes: {TimeSpent.Minutes}, Seconds {TimeSpent.Seconds}";
        }

        public override string ToString()
        {
            return $"{StudyCaseType}: for {TimeSpent.Hours} hours, {TimeSpent.Minutes} minutes and {TimeSpent.Seconds} seconds.";
        }

        #endregion

        #region Events

        #endregion
    }
}
