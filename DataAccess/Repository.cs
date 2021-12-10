namespace Holism.Entities.DataAccess;

public class Repository
{
    public static Repository<EntityType> EntityType
    {
        get
        {
            return new Repository<EntityType>(new EntitiesContext());
        }
    }
}
