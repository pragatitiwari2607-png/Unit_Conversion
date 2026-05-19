using FluentValidation;
using UnitConversionAPI.Models;

namespace UnitConversionAPI.Validators;

public class ConversionRequestValidator : AbstractValidator<ConversionRequest>
{
    private static readonly Dictionary<string, List<string>> AllowedUnits = new()
    {
        { "length",      new List<string> { "meter", "kilometer", "feet" } },
        { "temperature", new List<string> { "celsius", "fahrenheit" } },
        { "weight",      new List<string> { "kilogram", "gram", "pound" } }
    };

    public ConversionRequestValidator()
    {
        RuleFor(x => x.Category)
            .Must(c => !string.IsNullOrWhiteSpace(c)).WithMessage("Category is required.")
            .Must(c => AllowedUnits.ContainsKey(c.ToLower()))
            .WithMessage($"Category must be one of: {string.Join(", ", AllowedUnits.Keys)}.");

        RuleFor(x => x.FromUnit)
            .Must(f => !string.IsNullOrWhiteSpace(f)).WithMessage("FromUnit is required.");

        RuleFor(x => x.ToUnit)
            .Must(t => !string.IsNullOrWhiteSpace(t)).WithMessage("ToUnit is required.");

        RuleFor(x => x.Value)
            .Must(v => !double.IsNaN(v) && !double.IsInfinity(v))
            .WithMessage("Value must be a finite number.")
            .GreaterThanOrEqualTo(0).WithMessage("Value must be zero or greater.");

        RuleFor(x => x)
            .Must(BeValidUnits)
            .WithMessage("FromUnit or ToUnit is not valid for the given category.")
            .When(x => !string.IsNullOrWhiteSpace(x.Category)
                     && AllowedUnits.ContainsKey(x.Category.ToLower())
                     && !string.IsNullOrWhiteSpace(x.FromUnit)
                     && !string.IsNullOrWhiteSpace(x.ToUnit));

        RuleFor(x => x)
            .Must(x => !x.FromUnit.Equals(x.ToUnit, StringComparison.OrdinalIgnoreCase))
            .WithMessage("FromUnit and ToUnit cannot be the same.")
            .When(x => !string.IsNullOrWhiteSpace(x.FromUnit)
                     && !string.IsNullOrWhiteSpace(x.ToUnit));
    }

    private bool BeValidUnits(ConversionRequest request)
    {
        if (!AllowedUnits.TryGetValue(request.Category.ToLower(), out var units))
            return false;

        return units.Contains(request.FromUnit.ToLower())
            && units.Contains(request.ToUnit.ToLower());
    }
}
