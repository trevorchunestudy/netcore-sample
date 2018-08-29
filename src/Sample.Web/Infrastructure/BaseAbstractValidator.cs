using FluentValidation;

namespace Sample.Web.Infrastructure
{
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T> where T : BaseViewModel
    {
        protected const string REQUIRED = "Required";
    }
}
