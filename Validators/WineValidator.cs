namespace BD_La01.Validators;

public class WineValidator : AbstractValidator<Wine>
{
    public WineValidator()
    {
        RuleFor(w => w.Name).NotEmpty().MinimumLength(5).WithMessage("Name is required or too short");
        RuleFor(w => w.Year).NotEmpty().GreaterThan(1900).LessThan(2023).WithMessage("Year is required or invalid");
        RuleFor(w => w.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(w => w.Color).NotEmpty().WithMessage("Color is required");
        RuleFor(w => w.Price).NotEmpty().WithMessage("Price is required");
        RuleFor(w => w.Grapes).NotEmpty().WithMessage("Grapes is required");
    }
}
