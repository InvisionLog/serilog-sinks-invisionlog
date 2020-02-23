# serilog-sinks-invisionlog
A Serilog sink that writes events in periodic batches to [InvisionLog](https://www.invisionlog.com/).

**Package** - [Serilog.Sinks.InvisionLog](http://nuget.org/packages/serilog.sinks.invisionlog)  
**Platforms** - .NET Standard 2.0

```csharp
// Minimum default configuration
var log = new LoggerConfiguration()
    .WriteTo.InvisionLog(opts =>
    {
        opts.StaticKey = "[Static key]";
        opts.ApplicationKey = "[Optional Application Key]";
    })
    .CreateLogger();
```
