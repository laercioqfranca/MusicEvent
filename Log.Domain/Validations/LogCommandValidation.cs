using FluentValidation;
using Log.Domain.Commands;

namespace Log.Domain.Validations
{
    public class LogCommandValidation : CommandValidation<LogCreateCommand>
    {
        public LogCommandValidation()
        {
        }
    }
}
