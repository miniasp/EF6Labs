
namespace ConsoleApplication1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class ContosoUniversityEntities : DbContext
    {
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Modified)
                {
                    Console.WriteLine("Entity Name: {0}", entry.Entity.GetType().FullName);

                    entry.CurrentValues.SetValues(new {
                        ModifiedOn = DateTime.Now,
                        修改時間 = DateTime.Now
                    });
                }
            }

            return base.SaveChanges();
        }
    }
}
