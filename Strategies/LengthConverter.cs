using UnitConversionAPI.Strategies.Interfaces;

namespace UnitConversionAPI.Strategies;

public class LengthConverter : IUnitConverter
{
    public string Category => "length";

    private static readonly Dictionary<string, double> Factors = new()
    {
        { "meter", 1 },
        { "kilometer", 1000 },
        { "feet", 0.3048 }
    };

    public double Convert(string fromUnit, string toUnit, double value)
    {
        ValidateUnit(fromUnit);
        ValidateUnit(toUnit);

        var valueInMeters = value * Factors[fromUnit.ToLower()];
        return valueInMeters / Factors[toUnit.ToLower()];
    }

    private void ValidateUnit(string unit)
    {
        if (!Factors.ContainsKey(unit.ToLower()))
            throw new Exception($"Invalid length unit: {unit}");
    }
}