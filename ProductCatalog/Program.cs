using Microsoft.Extensions.Hosting;
using ProductCatalog.API.CategoryManagement.Facades;
using ProductCatalog.API.ProductManagement.Facades;
using ProductCatalog.API.ProductManagement.Services;
using ProductCatalog.Core;
using ProductCatalog.Persistence;
using ProductCatalog.Persistence.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.Configure<ConnectionStringConfig>(builder.Configuration.GetSection("ConnectionStrings"));

//TODO: v Alze používáme jiný způsob nalezení asembly
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<UpdateProductPriceService>();

// Project registration
builder.Services.AddScoped<IProductManagementFacade, ProductManagementFacade>();
builder.Services.AddScoped<ICategoryManagementFacade, CategoryManagementFacade>();

CoreRegistrator.RegisterEverything(builder.Services);
PersistanceRegistrator.RegisterEverything(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
