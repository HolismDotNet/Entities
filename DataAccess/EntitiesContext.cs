using System.Collections.Generic;
using Holism.Entities.Models;
using Holism.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Holism.Entities.DataAccess
{
    public class EntitiesContext : DatabaseContext
    {
        public override string ConnectionStringName => "Entities";

        public DbSet<Entity> Entities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
