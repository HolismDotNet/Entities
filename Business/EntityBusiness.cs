using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holism.Business;
using Holism.DataAccess;
using Holism.Entities.DataAccess;
using Holism.Entities.Models;
using Holism.Infra;

namespace Holism.Entities.Business
{
    public class EntityBusiness : Business<Entity, Entity>
    {
        protected override Repository<Entity> WriteRepository =>
            Repository.Entity;

        protected override ReadRepository<Entity> ReadRepository =>
            Repository.Entity;

        private static Dictionary<string, Guid> entityNames;

        private static Dictionary<Guid, string> entityGuids;

        private static Dictionary<string, Guid> EntityNames
        {
            get
            {
                if (entityNames == null)
                {
                    entityNames =
                        new EntityBusiness()
                            .GetAll()
                            .ToDictionary(i => i.Name.ToLower(), i => i.Guid);
                }
                return entityNames;
            }
        }

        private static Dictionary<Guid, string> EntityGuids
        {
            get
            {
                if (entityGuids == null)
                {
                    entityGuids =
                        new EntityBusiness()
                            .GetAll()
                            .ToDictionary(i => i.Guid, i => i.Name.ToLower());
                }
                return entityGuids;
            }
        }

        public Guid GetGuid(string name)
        {
            name = name.ToLower();
            if (EntityNames.ContainsKey(name))
            {
                return EntityNames[name];
            }
            throw new ServerException(@$"Entities {
                    name} is not specified in the database.");
        }

        public string GetName(Guid guid)
        {
            if (EntityGuids.ContainsKey(guid))
            {
                return EntityGuids[guid];
            }
            throw new ServerException(@$"No Entities is defined to have the guid {
                    guid}");
        }
    }
}
