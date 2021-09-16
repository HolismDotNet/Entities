using System;

namespace Holism.Entities.Models
{
    public class Entity : Holism.Models.IGuidEntity
    {
        public Entity()
        {
            RelatedItems = new System.Dynamic.ExpandoObject();
        }

        public long Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public dynamic RelatedItems { get; set; }
    }
}
