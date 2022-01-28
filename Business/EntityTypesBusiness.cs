namespace Entities;

public class EntityTypeBusiness : Business<EntityType, EntityType>
{
    protected override Repository<EntityType> WriteRepository => Repository.EntityType;

    protected override ReadRepository<EntityType> ReadRepository => Repository.EntityType;

    private static Dictionary<string, Guid> entityTypeNames;

    private static Dictionary<Guid, string> entityTypeGuids;

    private static Dictionary<string, Guid> EntityTypeNames
    {
        get
        {
            if (entityTypeNames == null)
            {
                entityTypeNames =
                    new EntityTypeBusiness()
                        .GetAll()
                        .ToDictionary(i => i.Name.ToLower(), i => i.Guid);
            }
            return entityTypeNames;
        }
    }

    private static Dictionary<Guid, string> EntityTypeGuids
    {
        get
        {
            if (entityTypeGuids == null)
            {
                entityTypeGuids =
                    new EntityTypeBusiness()
                        .GetAll()
                        .ToDictionary(i => i.Guid, i => i.Name.ToLower());
            }
            return entityTypeGuids;
        }
    }

    public Guid GetGuid(string name)
    {
        name = name.ToLower();
        if (EntityTypeNames.ContainsKey(name))
        {
            return EntityTypeNames[name];
        }
        throw new ServerException(@$"Entities {
                name} is not specified in the database.");
    }

    public string GetName(Guid guid)
    {
        if (EntityTypeGuids.ContainsKey(guid))
        {
            return EntityTypeGuids[guid];
        }
        throw new ServerException(@$"No Entities is defined to have the guid {
                guid}");
    }
}
