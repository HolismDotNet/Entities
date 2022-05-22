namespace Entities;

public class EntityTypeBusiness : Business<EntityType, EntityType>
{
    static Random random = new Random();

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

    protected override void ModifyItemBeforeReturning(EntityType item)
    {
        item.RelatedItems.DefaultImageUrl = Storage.GetImageUrl(item.Name, Guid.Empty);
        base.ModifyItemBeforeReturning(item);
    }

    public Guid GetGuid(string name)
    {
        name = name.ToLower();
        if (EntityTypeNames.ContainsKey(name))
        {
            return EntityTypeNames[name];
        }
        if (InfraConfig.IsDeveloping || AutomaticCreation)
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

    public void SetRandomDefaultImage(long id)
    {
        var entityType = Get(id);
        var width = random.Next(100,900);
        var height = random.Next(100, 900);
        var imageBytes = new System.Net.Http.HttpClient().GetAsync($"https://picsum.photos/{width}/{height}").Result.Content.ReadAsByteArrayAsync().Result;
        Storage.UploadImage(imageBytes, Guid.Empty, entityType.Name);
    }

    public void SetRandomDefaultImages(List<long> ids)
    {
        foreach (var id in ids)
        {
            SetRandomDefaultImage(id);
        }
    }
}
