namespace Entities;

public class EntitiesContext : DatabaseContext
{
    public override string ConnectionStringName => "Entities";

    public DbSet<Entities.EntityType> EntityTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
