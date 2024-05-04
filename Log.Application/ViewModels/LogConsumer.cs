using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Application.ViewModels
{
    public class LogConsumer : IConsumer<LogViewModel>
    {
        public Task Consume(ConsumeContext<LogViewModel> context)
        {
            Console.WriteLine(context.Message);
            Console.WriteLine("+++++++++++++++++++++++");
            return Task.CompletedTask;
        }
    }
}
