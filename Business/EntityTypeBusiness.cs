using Holism.Business;
using Holism.DataAccess;
using Holism.Infra;
using Holism.Entity.DataAccess;
using Holism.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holism.Entity.Business
{
    public class EntityTypeBusiness : Business<EntityType, EntityType>
    {
        private const string EntityTypeNamePropertyName = "EntityTypeName";

        private const string EntityTypeGuidPropertyName = "EntityTypeGuid";

        protected override Repository<EntityType> WriteRepository => Repository.EntityType;

        protected override ReadRepository<EntityType> ReadRepository => Repository.EntityType;

        private static Dictionary<string, Guid> entityTypeNames;

        private static Dictionary<Guid, string> entityTypeGuids;

        private static Dictionary<string, Guid> EntityTypeNames
        {
            get
            {
                if (entityTypeNames == null)
                {
                    entityTypeNames = new EntityTypeBusiness().GetAll().ToDictionary(i => i.Name.ToLower(), i => i.Guid);
                }
                return entityTypeNames;
            }
        }

        private static Dictionary<Guid, string> EntityTypeGuids
        {
            get
            {
                if (entityTypeGuids == null)
                {
                    entityTypeGuids = new EntityTypeBusiness().GetAll().ToDictionary(i => i.Guid, i => i.Name.ToLower());
                }
                return entityTypeGuids;
            }
        }

        public Guid GetGuid(string name)
        {
            name = name.ToLower();
            if (EntityTypeNames.ContainsKey(name))
            {
                return EntityTypeNames[name];
            }
            throw new ServerException($"Entity {name} is not specified in the database.");
        }

        public string GetName(Guid guid)
        {
            if (EntityTypeGuids.ContainsKey(guid))
            {
                return EntityTypeGuids[guid];
            }
            throw new ServerException($"No entity is defined to have the guid {guid}");
        }

        public void InflateWithEntityType(object[] entities)
        {
            if (entities.Length == 0)
            {
                return;
            }
            var relatedItemsProperty = entities.First().GetType().GetProperty("RelatedItems");
            var entityTypeNameProperty = entities.First().GetType().GetProperty("EntityTypeName");
            var entityTypeGuidProperty = entities.First().GetType().GetProperty("EntityTypeGuid");
            foreach (var entity in entities)
            {
                if (!ExpandoObjectExtensions.Has((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeNamePropertyName))
                {
                    ExpandoObjectExtensions.AddProperty((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeNamePropertyName, GetName((Guid)entityTypeGuidProperty.GetValue(entity)));
                }
                if (!ExpandoObjectExtensions.Has((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeGuidPropertyName))
                {
                    ExpandoObjectExtensions.AddProperty((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeGuidPropertyName, GetGuid((string)entityTypeNameProperty.GetValue(entity)));
                }
            }
        }

        public void InflateWithEntityType(object entity)
        {
            if (entity == null)
            {
                return;
            }
            var relatedItemsProperty = entity.GetType().GetProperty("RelatedItems");
            var entityTypeNameProperty = entity.GetType().GetProperty("EntityTypeName");
            var entityTypeGuidProperty = entity.GetType().GetProperty("EntityTypeGuid");
            if (!ExpandoObjectExtensions.Has((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeNamePropertyName))
            {
                ExpandoObjectExtensions.AddProperty((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeNamePropertyName, GetName((Guid)entityTypeGuidProperty.GetValue(entity)));
            }
            if (!ExpandoObjectExtensions.Has((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeGuidPropertyName))
            {
                ExpandoObjectExtensions.AddProperty((dynamic)relatedItemsProperty.GetValue(entity), EntityTypeGuidPropertyName, GetGuid((string)entityTypeNameProperty.GetValue(entity)));
            }
        }
    }
}
