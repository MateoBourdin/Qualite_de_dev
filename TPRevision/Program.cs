using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DbProduitsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IProduitRepository, ProduitRepository>();
builder.Services.AddScoped<IQueryService<ProduitDto, ProduitDetailDto>, ProduitRepository>();

builder.Services.AddScoped<IMarqueRepository, MarqueRepository>();
builder.Services.AddScoped<IQueryService<MarqueDto, MarqueDto>, MarqueRepository>();

builder.Services.AddScoped<ITypeProduitRepository, TypeProduitRepository>();
builder.Services.AddScoped<IQueryService<TypeProduitDto, TypeProduitDto>, TypeProduitRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
