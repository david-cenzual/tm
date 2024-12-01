using System.Text.Json.Serialization;
using UOC.Consent.Platform.ApiService.Behaviours;
using UOC.Consent.Platform.ApiService.Options;
using UOC.Consent.Platform.Application;
using UOC.Consent.Platform.Infrastructure;
using UOC.Consent.Platform.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder
    .Host
    .AddConsentLogs();

builder
    .Services
    .AddApplicationOptions()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddProblemDetails()
    .AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseExceptionHandler()
    .UseHttpsRedirection();

app.MapDefaultEndpoints();
app.AddConsentRequestTraces();
app.MapControllers();

app.Run();