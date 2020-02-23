using System;

namespace Serilog.Sinks.InvisionLog.Config
{
    public class InvisionLogSinkOptionsBuilder
    {
        /// <summary>
        /// You typically don't have to set this property.
        /// </summary>
        public string InvisionLogServerUrl { get; set; } = "https://log.invisionlog.com";

        /// <summary>
        /// You can find this key in your InvisionLog dashboard under Settings -> API Keys -> 'Static secret keys' input field.
        /// </summary>
        public string StaticKey { get; set; }

        /// <summary>
        /// Specify your custom API key. You can find this key in your InvisionLog dashboard under Settings -> API Keys -> Find your custom API key in the list.
        /// </summary>
        public string ApplicationKey { get; set; }

        public IFormatProvider FormatProvider { get; set; }
    }
}
