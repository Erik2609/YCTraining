namespace DataAcces
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StudyModel : DbContext
    {
        public StudyModel()
            : base("name=StudyModel")
        {
        }

        public virtual DbSet<StudyCase> StudyCases { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Type>()
                .HasMany(e => e.StudyCases)
                .WithRequired(e => e.Type)
                .WillCascadeOnDelete(false);
        }
    }
}
