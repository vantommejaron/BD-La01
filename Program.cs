var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<WineValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Wine API", Version = "v1" });
    c.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();
app.MapSwagger();
app.UseSwaggerUI();


// ------------------------
// Make a list with wines
// ------------------------

var wines = new List<Wine>();
//fill the list with some wines
wines.Add(new Wine { WineId = 1, Name = "Chateau de Sours", Year = 2012, Country = "France", Color = "Red", Price = 15, Grapes = "Merlot" });
wines.Add(new Wine { WineId = 2, Name = "Chateau du Tertre", Year = 2012, Country = "France", Color = "Red", Price = 42, Grapes = "Merlot" });
wines.Add(new Wine { WineId = 3, Name = "Chateau du Tertre", Year = 2012, Country = "France", Color = "Red", Price = 42, Grapes = "Merlot" });

// ------------------------
// PUT a wine with a id
// ------------------------
app.MapPut("/wine/{wineId}", (int wineId, Wine wine) =>
{
    var wineToUpdate = wines.FirstOrDefault(w => w.WineId == wineId);
    if (wineToUpdate == null)
    {
        return Results.NotFound();
    }
    wineToUpdate.Name = wine.Name;
    wineToUpdate.Year = wine.Year;
    wineToUpdate.Country = wine.Country;
    wineToUpdate.Color = wine.Color;
    wineToUpdate.Price = wine.Price;
    wineToUpdate.Grapes = wine.Grapes;
    return Results.Ok(wineToUpdate);
});

// ------------------------
// DELETE a wine with a id
// ------------------------
app.MapDelete("/wine/{wineId}", (int wineId) =>
{
    var wine = wines.FirstOrDefault(w => w.WineId == wineId);
    if (wine == null)
    {
        return Results.NotFound();
    }
    wines.Remove(wine);
    return Results.Ok();
});

// ------------------------
// ADD a wine
// ------------------------
app.MapPost("/wines", (IValidator<Wine> validator, Wine wine) =>
{
    var result = validator.Validate(wine);
    if (!result.IsValid)
    {
        return Results.BadRequest(result.Errors);
    }
    
    // this makes a id for the wine
    wine.WineId = wines.Max(w => w.WineId) + 1;

    wines.Add(wine);
    return Results.Created($"/wine/{wine.WineId}", wine);
});

// ------------------------
// 
// ------------------------
app.MapGet("/", () => "(☞ﾟヮﾟ)☞ Hello world ☜(ﾟヮﾟ☜)");

// ------------------------
// GET a wine with a id
// ------------------------
app.MapGet("/wine/{wineId}", (int wineId) =>
{
    var wine = wines.FirstOrDefault(w => w.WineId == wineId);
    if (wine == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(wine);
});

// ------------------------
// GET a wine
// ------------------------
app.MapGet("/wines", () =>
{
    return Results.Ok(new Wine
    {
        WineId = 1,
        Name = "Chateau de Sours",
        Year = 2012,
        Country = "France",
        Color = "Red",
        Price = 15,
        Grapes = "Merlot"
    });
});


app.Run();
