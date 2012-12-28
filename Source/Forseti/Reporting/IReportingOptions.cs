using System;
namespace Forseti.Reporting
{
    public interface IReportingOptions
    {
        bool OnlyOutputFailed { get; }
    }
}
