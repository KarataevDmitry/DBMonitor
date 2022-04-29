using DBMonitor.BLL;

using FluentValidation;
using FluentValidation.Validators;

namespace DBMonitor.API.Validators
{
    internal class CountQueryValidator : IPropertyValidator<Rule, string>
    {
        public string Name { get; }

        public string GetDefaultMessageTemplate(string errorCode) => throw new NotImplementedException();
        public bool IsValid(ValidationContext<Rule> context, string value)
        {
            var lower = value.ToLower();
            if (!lower.Contains("select"))
            {
                context.AddFailure("В тексте запроса обязательно должен быть select");
                return false;
            }

            if (!lower.Contains("count"))
            {
                context.AddFailure("В тексте запроса обязательно должен быть count");
                return false;
            }

            return true;
        }
    }
}