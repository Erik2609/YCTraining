using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor
{
    public class StudyCaseType : INotifyPropertyChanged
    {
        private List<string> _studyTypes;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> StudyTypes
        {
            get { return _studyTypes; }
            set { _studyTypes = value; }
        }
    }
}
