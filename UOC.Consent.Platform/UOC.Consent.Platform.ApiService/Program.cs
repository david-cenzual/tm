
using UOC.Consent.Platform.Application;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();


// Add services to the container.
builder.Services
       .AddEndpointsApiExplorer()
       .AddSwaggerGen()
       .AddApplication()
       .AddProblemDetails()
       .AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();