using Bodega;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SoyongContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SoyongDB")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Cambia esto al puerto de tu aplicación React
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDefaultFiles(); // Para buscar index.html
    app.UseStaticFiles();    
}
// Usar CORS
app.UseCors("AllowReact");
// Configurar el resto de los middlewares
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
// Mapeo de controladores
app.MapControllers();
// Si no encuentra la ruta en la API, redirige a React
app.MapFallbackToFile("/index.html");
app.Run();

