namespace Entities;

public class Repository
{
    public static Write<Entities.EntityType> EntityType
    {
        get
        {
            return new Write<Entities.EntityType>(new EntitiesContext());
        }
    }
}
