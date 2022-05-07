using Nis.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddHttpClient();
services
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
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
