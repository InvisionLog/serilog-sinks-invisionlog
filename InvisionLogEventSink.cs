using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.InvisionLog
{
    public class InvisionLogEventSink : PeriodicBatchingSink
    {
        private readonly string _staticKey;
        private readonly string _applicationKey;
        private readonly IFormatProvider _formatProvider;
        private readonly HttpClient _httpClient;

        public InvisionLogEventSink(string mapitomServerUrl, 
            string staticKey, 
            string applicationKey,
            IFormatProvider formatProvider) 
        : base(1000, TimeSpan.FromSeconds(2), 100000)
        {
            try
            {
                _staticKey = staticKey;
                _applicationKey = applicationKey;
                _formatProvider = formatProvider;
                _httpClient = new HttpClient { BaseAddress = new Uri(mapitomServerUrl.TrimEnd('/')) };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            var mapitomLogEvents = events.ToList().ConvertAll(logEvent =>
            {
                return new MapitomLogEvent
                {
                    LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), $"{logEvent.Level}"),
                    Message = logEvent.RenderMessage(_formatProvider),
                    DateTimeUtc = logEvent.Timestamp.DateTime.ToUniversalTime(),
                    Properties = logEvent.Properties.ToDictionary(p => p.Key, p => p.Value.ToString())
                };
            });

            var mapitomLogData = new MapitomLogData
            {
                Key = _staticKey,
                ApplicationKey = _applicationKey,
                MapitomLogEvents = mapitomLogEvents
            };

            var json = ConvertToJson(mapitomLogData);
            var uri = "/api/logger/logs";
            var task = await _httpClient.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        private string ConvertToJson(MapitomLogData mapitomLogEvents)
        {
            var json = JsonSerializer.Serialize(mapitomLogEvents, new JsonSerializerOptions
            {
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = true
            });

            return json;
        }
    }
}
