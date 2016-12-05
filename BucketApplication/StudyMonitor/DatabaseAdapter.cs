using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using DataAcces;

namespace StudyMonitor
{
    public class DatabaseAdapter
    {
        public static List<StudyCase> GetStudyCases()
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
            var dbStudyCase = new DataAcces.StudyCase()
            {
                Description = studyCase.Description,
                TimeSpent = new DateTime(2000,1,1, timeSpent.Hours, timeSpent.Minutes, timeSpent.Seconds),
                TypeId = DatabaseAcces.GetStudyTypeId(studyCase.StudyCaseType), 
            };
            DatabaseAcces.SaveStudyCase(dbStudyCase);
        }

        public static List<string> GetStudyTypes()
        {
            var dbStudyTypes = GetDBStudyTypes();
            var listStudyTypes = dbStudyTypes.Select(e => e.Type1);
            return listStudyTypes.ToList();
        } 


        private static List<StudyCase> ConvertDBStudyCase(DbSet<DataAcces.StudyCase> dbsetStudyCases)
        {
            List<StudyCase> convertedStudyCases = new List<StudyCase>();
            var StudyTypes = GetDBStudyTypes();
            foreach (var dbStudyCase in dbsetStudyCases)
            {
                var timeSpent = dbStudyCase.TimeSpent;
                convertedStudyCases.Add(
                    new StudyCase(dbStudyCase.Type.Type1,
                    dbStudyCase.Description,
                    new TimeSpan(timeSpent.Hour,
                    timeSpent.Minute,
                    timeSpent.Second)));
            }

            return convertedStudyCases;
        }

        private static DbSet<DataAcces.Type> GetDBStudyTypes()
        {
            return DatabaseAcces.TestGetStudyType();
        }   
    }
}
