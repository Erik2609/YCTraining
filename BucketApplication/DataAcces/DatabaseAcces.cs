using System;
using System.Data.Entity;
using System.Linq;

namespace DataAcces
{
    public class DatabaseAcces
    {
        private static StudyModel entities = new StudyModel();
        public static void TestAddStudyCase()
        {
            entities.StudyCases.Add(
                new StudyCase()
                {
                TimeSpent = new DateTime(2000,1,1,4,30,59),
                Description = "Learning c# in the lesson",
                TypeId = 1
                    
                });
            entities.SaveChanges();
        }

        public static DbSet<StudyCase> TestGetStudyCases()
        {
            return entities.StudyCases;
        }

        public static DbSet<Type> TestGetStudyType()
        {
            return entities.Types;
        }

        public static void SaveStudyCase(StudyCase studyCase)
        {
            entities.StudyCases.Add(studyCase);
            entities.SaveChanges();
        }

        public static int GetStudyTypeId(string Type)
        {
            return entities.Types.FirstOrDefault(e => e.Type1 == Type).Id;
        }
    }
}

