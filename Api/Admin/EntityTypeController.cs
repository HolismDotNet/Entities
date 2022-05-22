namespace Entities;

public class EntityTypeController : ReadController<EntityType>
{
    public override ReadBusiness<EntityType> ReadBusiness => new EntityTypeBusiness();

    [HttpPost]
    public IActionResult Fill()
    {
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