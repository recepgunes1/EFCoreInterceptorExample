using EFCoreInterceptorExample.Database;
using EFCoreInterceptorExample.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(p =>
        p.UseSqlServer(builder.Configuration.GetConnectionString("Database")!)
        .AddInterceptors(new LoggingInterceptor()));

var app = builder.Build();

app.MapGet("/init_db", async (AppDbContext dbContext, CancellationToken cancellationToken) =>
{
    await dbContext.Database.EnsureDeletedAsync(cancellationToken);
    await dbContext.Database.EnsureCreatedAsync(cancellationToken);
});

var userGroup = app.MapGroup("/user");

userGroup.MapGet("/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id) =>
{
    var entity = dbContext.Users.FirstOrDefault(p => p.Id == id);
    return entity;
});

userGroup.MapGet("/logs/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id) =>
{
    var entities = dbContext.Logs.Where(p => p.EntityName == typeof(User).Name && p.EntityId == id).OrderByDescending(p => p.CreatedDateTime);
    return entities;
});

userGroup.MapPost("/", (AppDbContext dbContext, [FromBody] User user) =>
{
    dbContext.Add(user);
    dbContext.SaveChanges();
    return user.Id;
});

userGroup.MapDelete("/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id) =>
{
    var entity = dbContext.Users.Find(id);
    dbContext.Users.Remove(entity);
    dbContext.SaveChanges();
});

userGroup.MapPut("/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id, [FromBody] User user) =>
{
    var entity = dbContext.Users.Find(id);
    entity.Name = user.Name;
    entity.Email = user.Email;
    dbContext.SaveChanges();
});

var bookGroup = app.MapGroup("/book");

bookGroup.MapGet("/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id) =>
{
    var entity = dbContext.Books.FirstOrDefault(p => p.Id == id);
    return entity;
});

bookGroup.MapGet("/logs/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id) =>
{
    var entities = dbContext.Logs.Where(p => p.EntityName == typeof(Book).Name && p.EntityId == id).OrderByDescending(p => p.CreatedDateTime);
    return entities;
});

bookGroup.MapPost("/", (AppDbContext dbContext, [FromBody] Book book) =>
{
    dbContext.Add(book);
    dbContext.SaveChanges();
    return book.Id;
});

bookGroup.MapDelete("/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id) =>
{
    var entity = dbContext.Books.Find(id);
    dbContext.Books.Remove(entity);
    dbContext.SaveChanges();
});

bookGroup.MapPut("/{id:guid}", (AppDbContext dbContext, [FromRoute] Guid id, [FromBody] Book book) =>
{
    var entity = dbContext.Books.Find(id);
    entity.Title = book.Title;
    entity.Description = book.Description;
    entity.Pages = book.Pages;
    dbContext.SaveChanges();
});

app.Run();
