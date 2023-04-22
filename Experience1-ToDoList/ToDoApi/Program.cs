using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using ToDoApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(option =>{
    option.AddPolicy("EnablePolicy",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoDbContext>();
var app = builder.Build();

app.UseCors("EnablePolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
   app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
}

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/items", (ToDoDbContext dbContext) =>
{
    return dbContext.Items.ToList();
});

app.MapPost("/items", async (ToDoDbContext dbContext, Item item) =>
{
    dbContext.Add(item);
    await dbContext.SaveChangesAsync();
    return item;
});

app.MapPut("/items/{id}", async (ToDoDbContext dbContext, [FromBody] Item item,int id) =>
{
    var oldItem = await dbContext.Items.FindAsync(id);
    if (oldItem == null)
        return Results.NotFound();
    // oldItem.Name = item.Name;
    oldItem.IsComplete = item.IsComplete;
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/items/{id}", async (ToDoDbContext dbContext, int id) =>
{
    var oldItem = await dbContext.Items.FindAsync(id);
    if (oldItem == null)
        return Results.NotFound();
    dbContext.Items.Remove(oldItem);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
