using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RazzieAwardsAnalyzer.Application.Interfaces;
using RazzieAwardsAnalyzer.Data.Context;
using RazzieAwardsAnalyzer.IoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("RazzieAwardsDB"));

DependencyContainer.RegisterServices(builder.Services);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RazzieAwards API", Version = "v1", Description = "API para análise de premiação da categoria Pior Filme do Golden Raspberry Awards." });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RazzieAwards API v1"));

app.MapControllers();
app.UseHttpsRedirection();

var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = scopeFactory?.CreateScope())
{
    try
    {
        var importFileService = scope?.ServiceProvider.GetRequiredService<IImportFileService>();
        if (importFileService != null)
            await importFileService.ImportFileAsync();
    }
    catch{}    
}

app.Run();

/// <summary>
/// Extensão de classe Program para testes de integração.
/// </summary>
public partial class Program { }