namespace DataAcces
{
    using System.Data.Entity;

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
