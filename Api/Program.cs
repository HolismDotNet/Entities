using Holism.Api;

namespace Holism.Entity.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Startup.AddControllerSearchAssembly(typeof(Controllers.EntityTypeController).Assembly);
            Holism.Api.Config.ConfigureEverything();
            Application.Run();
        }
    }
}
