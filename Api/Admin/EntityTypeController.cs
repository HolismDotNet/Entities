namespace Entities;

public class EntityTypeController : ReadController<EntityType>
{
    public override ReadBusiness<EntityType> ReadBusiness => new EntityTypeBusiness();

    [HttpPost]
    public IActionResult Fill()
    {
        return OkJson();
    }

    [HttpGet]
    public IActionResult PopulateDefaultImages()
    {
        return OkJson();
    }
}