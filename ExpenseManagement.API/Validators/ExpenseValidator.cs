using DataTransfer.Requests;
using FluentValidation;

namespace Application.Validators
{
    public class ExpenseValidator : AbstractValidator<ExpenseRequest>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Item).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);
        }
    }
}
