using Nis.Api.Options;
using Nis.Api.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddHttpClient();
services
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
    .Configure<MoodleOptions>(configuration.GetSection("Moodle"))
    .AddDatabase()
    .AddSwagger(configuration)
    .AddDirectoryBrowser()
    .AddAuthentication()
    .AddMoodle();

var application = builder.Build();

if (application.Environment.IsDevelopment())
    application
        .UseDeveloperExceptionPage()
        .UseSwaggerDocumentation();

var provider = new PhysicalFileProvider(builder.Environment.WebRootPath);

application
    .UseHttpsRedirection()
    .UseStaticFiles(new StaticFileOptions { FileProvider = provider })
    .UseDirectoryBrowser(new DirectoryBrowserOptions { FileProvider = provider })
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => endpoints.MapControllers());

application.Seed().Run();

public partial class Program;
