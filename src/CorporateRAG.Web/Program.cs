using CorporateRAG.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CorporateRAG.Application.Abstractions.Persistence;
using CorporateRAG.Infrastructure.Persistence.Repositories;
using CorporateRAG.Application.Abstractions.Storage;
using CorporateRAG.Infrastructure.Storage;
using CorporateRAG.Application.UseCases.Documents;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IChunkRepository, ChunkRepository>();

builder.Services.AddScoped<IFileStorage, LocalFileStorage>();
builder.Services.AddScoped<UploadDocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
