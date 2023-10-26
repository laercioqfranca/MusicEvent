using FluentValidation;
using MusicEvent.Core.Commands;

namespace MusicEvent.Domain.Validations
{
    public class CommandValidation<T> : AbstractValidator<T> where T : Command
    {
    }
}
