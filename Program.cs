// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<BloodMatchService>();

var app = builder.Build();

app.MapControllers();
app.Run();