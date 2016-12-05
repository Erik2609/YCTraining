namespace DataAcces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudyCases")]
    public partial class StudyCase
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public DateTime TimeSpent { get; set; }

        public virtual Type Type { get; set; }
    }
}
