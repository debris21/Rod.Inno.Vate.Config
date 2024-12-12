var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var startup = new StartUp();
startup.ConfigureServices(builder.Services);
var app = builder.Build();
var env = app.Environment;
startup.Configure(app, env);
app.Run();