using Holism.Entity.Models;
using Holism.DataAccess;

namespace Holism.Entity.DataAccess
{
    public class Repository
    {
        public static Repository<EntityType> EntityType
        {
            get
            {
                return new Holism.DataAccess.Repository<EntityType>(new EntityContext());
            }
        }
    }
}
