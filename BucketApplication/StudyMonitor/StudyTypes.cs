using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StudyMonitor
{
    public class StudyTypes : ObservableCollection<string>, INotifyPropertyChanged
    {
        private ObservableCollection<string> _studyTypesCollection;

        public ObservableCollection<string> StudyTypesCollection
        {
            get { return _studyTypesCollection; }
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StudyTypesCollection"));
                _studyTypesCollection = value;
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
