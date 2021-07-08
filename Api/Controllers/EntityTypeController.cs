using Holism.Api.Controllers;
using Holism.Business;
using Holism.Entity.Business;
using Holism.Entity.Models;

namespace Holism.Entity.Api.Controllers
{
    public class EntityTypeController : ReadController<EntityType>
    {
        public override ReadBusiness<EntityType> ReadBusiness => new EntityTypeBusiness();
    }
}
