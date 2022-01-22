namespace Entities;

public class Repository
{
    public static Repository<Entities.EntityType> EntityType
    {
        get
        {
            return new Repository<Entities.EntityType>(new EntitiesContext());
        }
    }
}
