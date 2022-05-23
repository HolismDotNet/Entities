namespace Entities;

public class EntityTypeController : ReadController<EntityType>
{
    public override ReadBusiness<EntityType> ReadBusiness => new EntityTypeBusiness();

    [HttpPost]
    public IActionResult FindAll()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var businessClasses = assemblies
            .SelectMany(i => i.GetTypes().Where(x => x.Name.EndsWith("Business")))
            .Where(i => i.BaseType != null)
            .Where(i => i.BaseType.Name.Contains("Business"))
            .Where(i => !i.BaseType.Name.Contains("EnumBusiness"))
            .ToList();
        var entityTypes = new List<string>();
        foreach (var businessClass in businessClasses)
        {
            var instance = Activator.CreateInstance(businessClass);
            var entityTypeProperty = instance.GetType().GetProperty("EntityType");
            var entityType = entityTypeProperty.GetValue(instance).ToString();
            entityTypes.Add(entityType);
        }
        new EntityTypeBusiness().CreateAllEntityTypes(entityTypes);
        return OkJson();
    }

    [HttpPost]
    public IActionResult SetRandomDefaultImage(long id)
    {
        if (!InfraConfig.IsDeveloping)
        {
            throw new ClientException("Not avaiable in the production");
        }
        new EntityTypeBusiness().SetRandomDefaultImage(id);
        return OkJson();
    }

    [HttpPost]
    public IActionResult SetRandomDefaultImages(List<long> ids)
    {
        if (!InfraConfig.IsDeveloping)
        {
            throw new ClientException("Not avaiable in the production");
        }
        new EntityTypeBusiness().SetRandomDefaultImages(ids);
        return OkJson();
    }
}