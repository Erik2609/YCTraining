using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StudyMonitor
{
    /// <summary>
    /// Interaction logic for StudyCaseControl.xaml
    /// </summary>
    public partial class StudyCaseControl : UserControl
    {
        public readonly StudyCase StudyCase;
        public StudyCaseControl(StudyCase studyCase)
        {
            StudyCase = studyCase;
            InitializeComponent();

            textBlock.Text = studyCase.ToString();
            this.button.Click += InfoButtonClick;

        }

        private void InfoButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            MessageBox.Show(StudyCase.Info);
        }
    }
}
