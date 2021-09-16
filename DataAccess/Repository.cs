using Holism.Entities.Models;
using Holism.DataAccess;

namespace Holism.Entities.DataAccess
{
    public class Repository
    {
        public static Repository<Entity> Entity
        {
            get
            {
                return new Holism.DataAccess.Repository<Entity
                >(new EntitiesContext());
            }
        }
    }
}
