using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Doctorly.CodeTest.REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
            return webHost;
        }
    }
}
