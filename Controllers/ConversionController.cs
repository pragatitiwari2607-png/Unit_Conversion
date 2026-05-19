using Microsoft.AspNetCore.Mvc;
using UnitConversionAPI.Models;
using UnitConversionAPI.Services.Interfaces;

namespace UnitConversionAPI.Controllers;

[ApiController]
[Route("api/v1/conversion")]
public class ConversionController : ControllerBase
{
    private readonly IConversionService _conversionService;

    public ConversionController(IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpPost]
    public IActionResult Convert(ConversionRequest request)
    {
        var result = _conversionService.Convert(
            request.Category,
            request.FromUnit,
            request.ToUnit,
            request.Value);

        return Ok(new ConversionResponse
        {
            Category = request.Category,
            FromUnit = request.FromUnit,
            ToUnit = request.ToUnit,
            OriginalValue = request.Value,
            ConvertedValue = result
        });
    }
}