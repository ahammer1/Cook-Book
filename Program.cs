using System.Text.Json.Serialization;
using Recipe.DTOs;
using Recipe.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7027")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<RecipeDbContext>(builder.Configuration["RecipeDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//Get User
app.MapGet("/api/users/{uid}", (RecipeDbContext db, string uid) =>
{
    User user = db.Users.FirstOrDefault(u => u.UID == uid);
    if (user == null)
    {
        return Results.NoContent();
    }
    return Results.Ok(user);
});

//Get Recipe
app.MapGet("/api/items", (RecipeDbContext db) =>
{
    var Items = db.Items.ToList();
    return Results.Ok(Items);
});

//Get Single Recipe
app.MapGet("/api/item/{id}", (RecipeDbContext db, int id) =>
{
    Item Item = db.Items.FirstOrDefault(e => e.Id == id);

    if (Item == null)
    {
        return Results.NoContent();
    }

    return Results.Ok(Item);
});

// Create Item
app.MapPost("/api/items", (RecipeDbContext db, CreateItemDTO itemDTO) =>
{
    try
    {
        var newItem = new Item
        {
            Name = itemDTO.Name,
            Description = itemDTO.Description,
            Image = itemDTO.Image,
            Type = itemDTO.Type,
            User = itemDTO.User,
            Ingredients = new List<Ingredients>()
        };

        // Map ingredients from DTO to actual model
        foreach (var ingredientDTO in itemDTO.Ingredients)
        {
            var ingredient = new Ingredients
            {
                Name = ingredientDTO.Name,
                Quantity = ingredientDTO.Quantity
            };
            newItem.Ingredients.Add(ingredient);
        }

        db.Items.Add(newItem);
        db.SaveChanges();

        int id = newItem.Id;

        return Results.Created($"/api/items/{id}", newItem);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        return Results.NoContent();
    }
});

// Delete Item  
app.MapDelete("/api/item/{id}", (RecipeDbContext db, int id) =>
{
    Item itemToDelete = db.Items.FirstOrDefault(e => e.Id == id);

    if (itemToDelete == null)
    {
        return Results.NotFound();
    }

    db.Remove(itemToDelete);
    db.SaveChanges();

    return Results.NoContent();
});

//Update Item
app.MapPut("/api/item/{id}", (RecipeDbContext db, int id, UpdateItemDTO itemDTO) =>
{
    var existingItem = db.Items.FirstOrDefault(e => e.Id == id);

    if (existingItem == null)
    {
        return Results.NotFound();
    }

    existingItem.Name = itemDTO.Name;
    existingItem.Description = itemDTO.Description;
    existingItem.Image = itemDTO.Image;

    db.SaveChanges();

    return Results.NoContent();
});



app.Run();
