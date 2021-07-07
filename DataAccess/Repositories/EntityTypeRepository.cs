using Holism.EntityFramework;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq.Expressions;
using Holism.Entity.DataAccess.DbContexts;

namespace Holism.Entity.DataAccess.Repositories
{
    public partial class EntityTypeRepository : Repository<Holism.Entity.DataAccess.Models.EntityType>
    {
        public EntityTypeRepository(string databaseName = null)
            : base(new EntityTypeDbContext(databaseName))
        {
        }

        public override DataTable ConfigureDataTable()
        {
            var table = new DataTable();

			table.Columns.Add("Id", typeof(long));
			table.Columns.Add("Guid", typeof(Guid));
			table.Columns.Add("Name", typeof(string));

            return table;
        }

        public override void AddRecord(DataTable table, Holism.Entity.DataAccess.Models.EntityType entityType)
        {
            var row = table.NewRow();

			row["Id"] = (object)entityType.Id ?? DBNull.Value;
			row["Guid"] = (object)entityType.Guid ?? DBNull.Value;
			row["Name"] = (object)entityType.Name ?? DBNull.Value;

            table.Rows.Add(row);
        }

        public override void AddColumnMappings(SqlBulkCopy bulkOperator)
        {
			bulkOperator.ColumnMappings.Add("Id", "[Id]");
			bulkOperator.ColumnMappings.Add("Guid", "[Guid]");
			bulkOperator.ColumnMappings.Add("Name", "[Name]");
        }

        public override string BulkUpdateComparisonKey
        {
            get
            {
                return "(t.[Id] = s.[Id])";;
            }
        }

        public override string BulkUpdateInsertClause
        {
            get
            {
                return "([Guid], [Name]) values (s.[Guid], s.[Name])";
            }
        }

        public override string BulkUpdateUpdateClause
        {
            get
            {
                return "t.[Guid] = s.[Guid], t.[Name] = s.[Name]";
            }
        }

        public override string TableName
        {
            get
            {
                return "[EntityTypes]";
            }
        }

        public override Expression<Func<Holism.Entity.DataAccess.Models.EntityType, bool>> ExistenceFilter(Holism.Entity.DataAccess.Models.EntityType t)
        {
            Expression<Func<Holism.Entity.DataAccess.Models.EntityType, bool>> result = null;
            if (t.Id > 0)
            {
                result = i => i.Id == t.Id;
            }
            else
            {
                result = i => i.Id == t.Id;
            }
            return result;
        }

        public override string TempTableCreationScript(string tempTableName)
        {
            var tempTableScript =  $@"
                    create table {tempTableName}
                    (
						[Id] bigint not null,
						[Guid] uniqueidentifier not null,
						[Name] varchar(100) not null,    
                    )
                    ";
            return tempTableScript;
        }
    }
}
