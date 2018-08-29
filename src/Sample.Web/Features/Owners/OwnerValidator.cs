using FluentValidation;
using Sample.Web.Infrastructure;

namespace Sample.Web.Features.Owners
{
    public class OwnerValidator : BaseAbstractValidator<OwnerViewModel>
    {
        public OwnerValidator()
        {
            RuleFor(m => m.Name)
                .Length(1, 100)
                .NotEmpty().WithMessage(REQUIRED);
            

            //Add more rules heres
        }
    }
}
