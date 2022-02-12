namespace Entities;

public class EntityTypeBusiness : Business<EntityType, EntityType>
{
    public static bool AutomaticCreation = false;

    protected override Write<EntityType> Write => Repository.EntityType;

    protected override Read<EntityType> Read => Repository.EntityType;

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

    private void ResetCache()
    {
        entityTypeNames = null;
        entityTypeGuids = null;
    }

    public Guid GetGuid(string name)
    {
        name = name.ToLower();
        if (EntityTypeNames.ContainsKey(name))
        {
            return EntityTypeNames[name];
        }
        if (AutomaticCreation)
        {
            var entityType = Read.Get(i => i.Name.ToLower() == name);
            if (entityType != null)
            {
                ResetCache();
                return GetGuid(name);
            }
            entityType = new EntityType();
            entityType.Name = name;
            Create(entityType);
            return GetGuid(name);
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
