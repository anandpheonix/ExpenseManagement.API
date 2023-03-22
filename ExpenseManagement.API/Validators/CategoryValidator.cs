using DataTransfer.Requests;
using FluentValidation;

namespace Application.Validators;

public class CategoryValidator : AbstractValidator<CategoryRequest>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
    }
}
