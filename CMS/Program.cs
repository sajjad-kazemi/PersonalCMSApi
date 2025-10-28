
using Services.Extensions;

namespace CMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddApplicationServices(builder);
			var app = builder.Build();
            app.ConfigureApplication();
            app.Run();
        }
    }
}
