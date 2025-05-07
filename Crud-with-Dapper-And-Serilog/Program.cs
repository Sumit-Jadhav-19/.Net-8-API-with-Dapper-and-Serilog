using Crud_with_Dapper_And_Serilog.Data;
using Crud_with_Dapper_And_Serilog.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();  // Important

Log.Logger.Error("Loger implemented!");

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<CategoriesRepository>();
builder.Services.AddScoped<ProductRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
