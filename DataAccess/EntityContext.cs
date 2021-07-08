using Holism.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Holism.Entity.Models;

namespace Holism.Entity.DataAccess
{
    public class EntityContext : DatabaseContext
    {
        public override string ConnectionStringName => "Entity";

        public DbSet<EntityType> EntityTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
