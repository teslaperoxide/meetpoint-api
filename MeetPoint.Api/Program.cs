
using MeetPoint.Api.Interfaces;
using MeetPoint.Api.Services;
using MeetPoint.Api.Providers;

var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddControllers();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IVenueProvider, MockVenueProvider>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
