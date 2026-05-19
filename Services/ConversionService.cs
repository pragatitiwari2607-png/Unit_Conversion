using UnitConversionAPI.Services.Interfaces;
using UnitConversionAPI.Strategies.Interfaces;

namespace UnitConversionAPI.Services;

public class ConversionService : IConversionService
{
    private readonly IEnumerable<IUnitConverter> _converters;

    public ConversionService(IEnumerable<IUnitConverter> converters)
    {
        _converters = converters;
    }

    public double Convert(string category, string fromUnit, string toUnit, double value)
    {
        var converter = _converters.FirstOrDefault(x =>
            x.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        if (converter == null)
            throw new Exception($"Category not supported: {category}");

        return converter.Convert(fromUnit, toUnit, value);
    }
}