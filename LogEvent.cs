using System;
using System.Collections.Generic;

namespace Serilog.Sinks.InvisionLog
{
    public class MapitomLogData
    {
        public string Key { get; set; }
        public string ApplicationKey { get; set; }
        public List<MapitomLogEvent> MapitomLogEvents { get; set; } = new List<MapitomLogEvent>();
    }

    public class MapitomLogEvent
    {
        public string Message { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;
        public DateTime DateTimeUtc { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }

    public class Property
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public enum LogLevel
    {
        Verbose = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4,
        Fatal = 5
    }
}
