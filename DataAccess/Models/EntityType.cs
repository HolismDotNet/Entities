using System;

namespace Holism.Entity.DataAccess.Models
{
    public class EntityType : Holism.EntityFramework.IEntity
    {
        public EntityType()
        {
            RelatedItems = new System.Dynamic.ExpandoObject();
        }

        public long Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public dynamic RelatedItems { get; set; }
    }
}
