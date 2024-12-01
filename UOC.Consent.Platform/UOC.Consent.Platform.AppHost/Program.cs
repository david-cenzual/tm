var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder
    .AddProject<Projects.UOC_Consent_Platform_ApiService>("apiservice");

builder.AddProject<Projects.UOC_Consent_Platform_Web>("webfrontend")
       .WithExternalHttpEndpoints()
       .WithReference(apiService);

builder.Build().Run();