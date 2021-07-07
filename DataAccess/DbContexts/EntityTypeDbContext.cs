using Holism.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Holism.Entity.DataAccess.DbContexts
{
    public class EntityTypeDbContext : DbContext
    {
        string databaseName;

        public EntityTypeDbContext()
            : base()
        {
        }

        public EntityTypeDbContext(string databaseName)
            : base()
        {
            this.databaseName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.GetConnectionString(databaseName ?? Config.DatabaseName)).AddInterceptors(new PersianInterceptor());
        }

        public ICollection<Holism.Entity.DataAccess.Models.EntityType> EntityTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Holism.Entity.DataAccess.Models.EntityType>().ToTable("EntityTypes");
            modelBuilder.Entity<Holism.Entity.DataAccess.Models.EntityType>().Ignore(i => i.RelatedItems);
            base.OnModelCreating(modelBuilder);
        }
    }
}
