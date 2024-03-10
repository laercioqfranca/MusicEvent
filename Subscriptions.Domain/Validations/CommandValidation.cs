using FluentValidation;
using Subscriptions.Core.Commands;

namespace Subscriptions.Domain.Validations
{
    public class CommandValidation<T> : AbstractValidator<T> where T : Command
    {
    }
}
