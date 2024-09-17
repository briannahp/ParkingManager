using Microsoft.EntityFrameworkCore;
using ParkingAPI.Services; 
using ParkingAPI.AppData;
using Microsoft.Data.Sqlite;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var conn = new SqliteConnection();
conn.Open();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
    });

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
; 
builder.Services.AddDbContext<AccountContext>(options =>
{
    options.UseSqlite(conn);
    options.EnableSensitiveDataLogging();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IParkingSpaceService, ParkingSpaceService>();
builder.Services.AddScoped<IResidentService, ResidentService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllOrigins");
app.MapControllers(); 

app.Run();
