using Holism.Business;
using Holism.EntityFramework;
using Holism.Framework;
using Holism.Framework.Extensions;
using Holism.Entity.DataAccess;
using Holism.Entity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holism.Entity.Business
{
    public class EntityTypeBusiness : Business<EntityType, EntityType>
    {
        private const string EntityTypeNamePropertyName = "EntityTypeName";

        private const string EntityTypeGuidPropertyName = "EntityTypeName";

        protected override Repository<EntityType> ModelRepository => RepositoryFactory.EntityTypeFrom(databaseName);

        protected override ViewRepository<EntityType> ViewRepository => RepositoryFactory.EntityTypeFrom(databaseName);

        string databaseName;

        public EntityTypeBusiness(string databaseName = null)
        {
            this.databaseName = databaseName;
        }

        private static Dictionary<string, Dictionary<string, Guid>> databaseEntityTypeNames;

        private static Dictionary<string, Dictionary<Guid, string>> databaseEntityTypeGuids;

        private static Dictionary<string, Guid> EntityTypeNames(string databaseName)
        {
            if (databaseName.IsNothing())
            {
                // throw new FrameworkException($"Database is not specified. To work with entitites, you should specify the requested database.");
                databaseName = Framework.Config.GetSetting("EntityDatabaseName");
            }
            if (databaseEntityTypeNames.IsNull())
            {
                databaseEntityTypeNames = new Dictionary<string, Dictionary<string, Guid>>();
            }
            if (!databaseEntityTypeNames.ContainsKey(databaseName))
            {
                databaseEntityTypeNames.Add(databaseName, new EntityTypeBusiness(databaseName).GetAll().ToDictionary(i => i.Name.ToLower(), i => i.Guid));
            }
            return databaseEntityTypeNames[databaseName];
        }

        private static Dictionary<Guid, string> EntityTypeGuids(string databaseName)
        {
            if (databaseEntityTypeGuids.IsNull())
            {
                databaseEntityTypeGuids = new Dictionary<string, Dictionary<Guid, string>>();
            }
            if (!databaseEntityTypeGuids.ContainsKey(databaseName))
            {
                databaseEntityTypeGuids.Add(databaseName, new EntityTypeBusiness(databaseName).GetAll().ToDictionary(i => i.Guid, i => i.Name.ToLower()));
            }
            return databaseEntityTypeGuids[databaseName];
        }

        public Guid GetGuid(string name)
        {
            name = name.ToLower();
            if (EntityTypeNames(databaseName).ContainsKey(name))
            {
                return EntityTypeNames(databaseName)[name];
            }
            try
            {
                var model = new EntityType();
                model.Name = name;
                model.Guid = Guid.NewGuid();
                ModelRepository.Create(model);
                databaseEntityTypeNames = null;
                return EntityTypeNames(databaseName)[name];
            }
            catch (Exception ex)
            {
                Logger.LogWarning("A temporary hack for social module.");
                databaseEntityTypeNames = null;
                return GetGuid(name);
            }
        }

        public string GetName(Guid guid)
        {
            if (EntityTypeGuids(databaseName).ContainsKey(guid))
            {
                return EntityTypeGuids(databaseName)[guid];
            }
            throw new BusinessException($"No entity is defined to have the guid {guid}");
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
            if (entity.IsNull())
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
