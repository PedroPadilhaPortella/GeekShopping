using ChatGPT.ChatBotAPI.Extensions;

var appName = "ChatGPT ASP.NET 8 Integration";

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog(builder.Configuration, appName);

builder.AddChatGpt(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();

builder.Services.AddSwagger(builder.Configuration, appName);

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

app.UseSwaggerDoc(appName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();