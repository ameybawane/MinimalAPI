using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SuperHeroContext>(options =>
{
    options.UseInMemoryDatabase("SuperHeroDb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet("/", () => "Welcome To SUPERHERO database!");

// Get all SuperHero
app.MapGet("/superhero", (SuperHeroContext context) =>
 context.SuperHeros.ToList());

// Get SuperHero by Id
app.MapGet("/superhero/{id}", (SuperHeroContext context, int id) =>
 // check hero is NULL 
 context.SuperHeros.Find(id) is SuperHero hero ?
 // if hero is NOT NULL
 Results.Ok(hero) :
 // if hero is NULL
 Results.NotFound("Sorry, hero not found."));

// Add SuperHero
app.MapPost("/superhero/", (SuperHeroContext context, SuperHero hero) =>
{
    context.SuperHeros.Add(hero);
    context.SaveChanges();
    return Results.Ok(hero);
});

// Update SuperHero by Id
app.MapPut("/superhero/{id}", (SuperHeroContext context, SuperHero hero, int id) =>
{
    var dbHero = context.SuperHeros.Find(id);
    if (dbHero == null) return Results.NotFound("Sorry, hero not found.");

    dbHero.FirstName = hero.FirstName;
    dbHero.LastName = hero.LastName;
    dbHero.HeroName = hero.HeroName;
    context.SaveChanges();
    return Results.Ok(dbHero);
});

// Delete SuperHero by Id
app.MapDelete("/superhero/{id}", (SuperHeroContext context, int id) =>
{
    var dbHero = context.SuperHeros.Find(id);
    if (dbHero == null) return Results.NotFound("Sorry, hero not found.");
    context.SuperHeros.Remove(dbHero);
    context.SaveChanges();
    return Results.Ok(dbHero);
});

app.Run();