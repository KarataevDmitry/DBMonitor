using DBMonitor.BLL;

using FluentValidation;

namespace DBMonitor.API.Validators
{
    public class RuleValidator : AbstractValidator<Rule>
    {
        public RuleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.QueryText).SetValidator(new CountQueryValidator());
        }
    }
}
