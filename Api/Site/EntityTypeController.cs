namespace Api;

public class EntityTypeController : ReadController<EntityType>
{
    public override ReadBusiness<EntityType> ReadBusiness => new EntityTypeBusiness();
}