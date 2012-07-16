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
    }
}
