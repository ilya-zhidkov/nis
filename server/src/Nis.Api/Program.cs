using Nis.Api.Options;
using Nis.Api.Extensions;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddHttpClient();
services
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
    .Configure<MoodleOptions>(builder.Configuration.GetSection("Moodle"))
    .AddDatabase()
    .AddSwagger(builder.Configuration)
    .AddDirectoryBrowser();

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
    .UseEndpoints(endpoints => endpoints.MapControllers());

application.Seed().Run();
