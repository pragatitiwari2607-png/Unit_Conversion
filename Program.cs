using FluentValidation;
using FluentValidation.AspNetCore;
using UnitConversionAPI.Middleware;
using UnitConversionAPI.Services;
using UnitConversionAPI.Services.Interfaces;
using UnitConversionAPI.Strategies;
using UnitConversionAPI.Strategies.Interfaces;
using UnitConversionAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ConversionRequestValidator>();

builder.Services.AddScoped<IUnitConverter, LengthConverter>();
builder.Services.AddScoped<IUnitConverter, TemperatureConverter>();
builder.Services.AddScoped<IUnitConverter, WeightConverter>();

builder.Services.AddScoped<IConversionService, ConversionService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();