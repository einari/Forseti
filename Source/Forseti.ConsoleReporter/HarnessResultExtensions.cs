using System;
using Forseti.Suites;

namespace Forseti.ConsoleReporter
{
    public static class HarnessResultExtensions
    {
        public static string FriendlyName(this Case @case)
        {
            return ToFriendlyName(@case.Name);
        }

        static string ToFriendlyName(string name)
        {
            return string.IsNullOrEmpty(name) ? String.Empty : name.Replace("_", " ");
        }

        public static string FriendlyName(this Description description)
        {
            return ToFriendlyName(description.Name);
        }

        public static string FriendlyName(this Suite suite) 
        {
            return ToFriendlyName(suite.System);
        
        }
    }
}
