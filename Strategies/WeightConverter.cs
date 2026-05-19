using UnitConversionAPI.Strategies.Interfaces;

namespace UnitConversionAPI.Strategies;

public class WeightConverter : IUnitConverter
{
    public string Category => "weight";

    private static readonly Dictionary<string, double> Factors = new()
    {
        { "kilogram", 1 },
        { "gram", 0.001 },
        { "pound", 0.453592 }
    };

    public double Convert(string fromUnit, string toUnit, double value)
    {
        ValidateUnit(fromUnit);
        ValidateUnit(toUnit);

        var valueInKg = value * Factors[fromUnit.ToLower()];
        return valueInKg / Factors[toUnit.ToLower()];
    }

    private void ValidateUnit(string unit)
    {
        if (!Factors.ContainsKey(unit.ToLower()))
            throw new Exception($"Invalid weight unit: {unit}");
    }
}