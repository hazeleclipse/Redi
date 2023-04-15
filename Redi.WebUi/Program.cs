using Redi.Application.Extensions;
using Redi.Infrastructure.Extensions;
using Redi.WebUi.Extensions;

namespace Redi.WebUi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration)
                    .AddWebUi();                
            }

            var app = builder.Build();
            {
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.UseAuthentication();                
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthorization();
                app.MapRazorPages();
                app.Run();
            }
        }
    }
}