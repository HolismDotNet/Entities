namespace Entities;

public class EntityType : IEntity, IGuid
{
    public EntityType()
    {
        RelatedItems = new ExpandoObject();
    }

    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; }

    public dynamic RelatedItems { get; set; }
}
