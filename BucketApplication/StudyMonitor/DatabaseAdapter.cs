using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DataAcces;

namespace StudyMonitor
{
    public class DatabaseAdapter
    {
        public static ObservableCollection<StudyCase> GetStudyCases()
        {

            var listOfStudyCases = DatabaseAcces.TestGetStudyCases();
            foreach (var studyCase in listOfStudyCases)
            {
                Debug.WriteLine(studyCase);
            }

            return ConvertDBStudyCase(listOfStudyCases);
        }

        public static void SaveToDatabase(StudyCase studyCase)
        {
            var timeSpent = studyCase.TimeSpent;
            var dateOfStudy = studyCase.DateOfStudy;
            var dbStudyCase = new DataAcces.StudyCase()
            {
                Description = studyCase.Description,
                TimeSpent = new DateTime(dateOfStudy.Year, dateOfStudy.Month, dateOfStudy.Day, timeSpent.Hours, timeSpent.Minutes, timeSpent.Seconds),
                TypeId = DatabaseAcces.GetStudyTypeId(studyCase.StudyCaseType), 
            };
            DatabaseAcces.SaveStudyCase(dbStudyCase);
        }

        public static ObservableCollection<string> GetStudyTypes()
        {
            var dbStudyTypes = GetDBStudyTypes();
            var studyTypesQuery = dbStudyTypes.Select(e => e.Type1);
            return new ObservableCollection<string>(studyTypesQuery.ToList());
        } 


        private static ObservableCollection<StudyCase> ConvertDBStudyCase(DbSet<DataAcces.StudyCase> dbsetStudyCases)
        {
            List<StudyCase> convertedStudyCases = new List<StudyCase>();
            foreach (var dbStudyCase in dbsetStudyCases)
            {
                var timeSpent = dbStudyCase.TimeSpent;
                var dateOfStudy = new DateTime(dbStudyCase.TimeSpent.Year, dbStudyCase.TimeSpent.Month, dbStudyCase.TimeSpent.Day);
                convertedStudyCases.Add(
                    new StudyCase(dbStudyCase.Type.Type1,
                    dbStudyCase.Description,
                    new TimeSpan(timeSpent.Hour,
                    timeSpent.Minute,
                    timeSpent.Second), dateOfStudy));
            }

            return new ObservableCollection<StudyCase>(convertedStudyCases);
        }

        private static DbSet<DataAcces.Type> GetDBStudyTypes()
        {
            return DatabaseAcces.TestGetStudyType();
        }   
    }
}
