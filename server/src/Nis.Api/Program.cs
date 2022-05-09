using Nis.Api.Options;
using Nis.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddHttpClient();
services
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
    .Configure<MoodleOptions>(builder.Configuration.GetSection("Moodle"))
    .AddDatabase()
    .AddSwagger(builder.Configuration);

var application = builder.Build();

if (application.Environment.IsDevelopment())
    application
        .UseDeveloperExceptionPage()
        .UseSwaggerDocumentation();

application
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseEndpoints(endpoints => endpoints.MapControllers());

application.Seed().Run();
