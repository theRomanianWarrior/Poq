using Microsoft.Extensions.Hosting;
using PoqAssignment.API.Infrastructure;

namespace PoqAssignment.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Bootstrap.CreateHostBuilder(args);
            builder.Build().Run();
        }
    }
}