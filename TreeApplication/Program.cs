using TreeApplication.DAL.Extensions;
using TreeApplication.Extensions;
using TreeApplication.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddLogger();

builder.Services.AddControllers();

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
