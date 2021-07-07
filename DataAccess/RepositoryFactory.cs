namespace Holism.Entity.DataAccess
{
    public class RepositoryFactory
    {
        public static Repositories.EntityTypeRepository EntityType
        {
            get
            {
                return new Repositories.EntityTypeRepository();
            }
        }

        public static Repositories.EntityTypeRepository EntityTypeFrom(string databaseName = null)
        {
            return new Repositories.EntityTypeRepository(databaseName);
        }
    }
}
