namespace UnitConversionAPI.Strategies.Interfaces;

public interface IUnitConverter
{
    string Category { get; }
    double Convert(string fromUnit, string toUnit, double value);
}