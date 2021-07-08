using System;

namespace Holism.Entity.Models
{
    public class EntityType : Holism.Models.IEntity
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
