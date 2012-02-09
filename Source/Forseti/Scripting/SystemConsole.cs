using System;

namespace Forseti.Scripting
{
    public class SystemConsole
    {
        public static bool LoggingEnabled = true;

        public static void Print(string message)
        {
            if (!LoggingEnabled)
                return;

            Console.WriteLine(message);
        }

        public static void ReportFailedCase(string description, string message) 
        {
            if (!LoggingEnabled)
                return;

            Console.WriteLine(string.Format(" Spec( {0} ) FAILED with message : {1}", description, message));
        } 
        
        public static void ReportPassedCase(string description) 
        {
            if (!LoggingEnabled)
                return;

            Console.WriteLine(string.Format(" Spec( {0} ) PASSED", description));
        }
    }
}
