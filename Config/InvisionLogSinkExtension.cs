using System;
using Serilog.Configuration;

namespace Serilog.Sinks.InvisionLog.Config
{
    public static class InvisionLogSinkExtension
    {
        public static LoggerConfiguration InvisionLog(this LoggerSinkConfiguration loggerConfiguration,
            Action<InvisionLogSinkOptionsBuilder> options)
        {
            var optionsBuilder = new InvisionLogSinkOptionsBuilder();
            options(optionsBuilder);

            return loggerConfiguration.Sink(new InvisionLogEventSink(optionsBuilder.InvisionLogServerUrl,
                optionsBuilder.StaticKey,
                optionsBuilder.ApplicationKey,
                optionsBuilder.FormatProvider));
        }
    }
}
