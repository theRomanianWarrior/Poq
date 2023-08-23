using FluentValidation;
using PoqAssignment.Application.DTO;

namespace PoqAssignment.Application.Validators
{
    public class FilterByUserValidator : AbstractValidator<FilterByUser>
    {
        public FilterByUserValidator()
        {
            RuleFor(filter => filter.MinPrice)
                .LessThanOrEqualTo(filter => filter.MaxPrice)
                .When(filter => filter.MinPrice.HasValue && filter.MaxPrice.HasValue)
                .WithMessage("MinPrice must be less than or equal to MaxPrice");

            RuleFor(filter => filter.MinPrice)
                .GreaterThanOrEqualTo(1)
                .LessThan(decimal.MaxValue)
                .WithMessage("MinPrice should be in a valid range");
            
            RuleFor(filter => filter.MaxPrice)
                .GreaterThanOrEqualTo(1)
                .LessThan(decimal.MaxValue)
                .WithMessage("MaxPrice should be in a valid range");
            
            RuleFor(filter => filter.Size)
                .IsInEnum()
                .WithMessage("Invalid Size value");

            RuleFor(filter => filter.Highlight)
                .NotEmpty()
                .WithMessage("Highlight cannot be empty");
        }
    }
}