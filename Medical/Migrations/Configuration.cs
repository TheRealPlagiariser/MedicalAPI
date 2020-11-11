namespace Medical.Migrations
{

    using Medical.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Medical.Models.MedicalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Medical.Models.MedicalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            //context.hrs.AddOrUpdate(
            //  p => p.emp_id,
            //  new hr()
            //  {
            //      emp_id = 1,
            //      password = "12345"
            //  }
            // );

           

            //context.Batches.AddOrUpdate(
            //    p => p.batch_id,
            //    new Batch() { batch_id = 1,  batch_date_from = DateTime.Now, batch_date_to = null }
            //    );

        }
    }
}
