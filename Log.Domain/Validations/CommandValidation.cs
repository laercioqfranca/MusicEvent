using FluentValidation;
using Log.Core.Commands;

namespace Log.Domain.Validations
{
    public class CommandValidation<T> : AbstractValidator<T> where T : Command
    {
    }
}
