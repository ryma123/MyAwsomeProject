namespace BusinessLayer.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Relation : DbContext
    {

        public Relation()
            : base("name=Relation")
        {
        }

        public DbSet<product> product { get; set; }
        public DbSet<Release> release { get; set; }
        public DbSet<KPI> kpi { get; set; }

    }
}