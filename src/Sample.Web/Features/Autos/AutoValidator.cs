using FluentValidation;
using Sample.Web.Infrastructure;

namespace Sample.Web.Features.Autos
{
    public class AutoValidator : BaseAbstractValidator<AutoViewModel>
    {
        public AutoValidator()
        {
            RuleFor(m => m.Title).NotEmpty().WithMessage(REQUIRED);
            

            //Add more rules heres
        }
    }
}
