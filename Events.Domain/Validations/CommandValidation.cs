using FluentValidation;
using Events.Core.Commands;

namespace Events.Domain.Validations
{
    public class CommandValidation<T> : AbstractValidator<T> where T : Command
    {
    }
}
