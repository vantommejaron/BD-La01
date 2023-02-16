var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "༼ つ ◕_◕ ༽つ");

app.Run();
