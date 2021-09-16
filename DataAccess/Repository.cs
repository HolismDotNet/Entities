using Holism.Entities.Models;
using Holism.DataAccess;

namespace Holism.Entities.DataAccess
{
    public class Repository
    {
        public static Repository<EntityType> EntityType
        {
            get
            {
                return new Holism.DataAccess.Repository<EntityType
                >(new EntitiesContext());
            }
        }
    }
}
