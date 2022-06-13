using projectef;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB")); // base de datos en memoria

builder.Services.AddSqlServer<TareasContext>("Data Source=DESKTOP-L67MPRC\\SQLEXPRESS; Initial Catalog= TareasDb; user id=sa; password=Samimajo10"); // conexion a el servidor

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion",async([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Basede datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();
